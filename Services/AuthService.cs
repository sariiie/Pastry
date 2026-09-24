using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MaisonFleurie.Data;
using MaisonFleurie.Models;

namespace MaisonFleurie.Services;

public class AuthService(IDbContextFactory<AppDbContext> dbFactory, ProtectedLocalStorage storage)
{
    private const string Key = "maisonfleurie.session";
    private readonly PasswordHasher<AppUser> hasher = new();
    private bool loaded;

    public AppUser? CurrentUser { get; private set; }
    public bool IsGuest { get; private set; }
    public bool HasSession => CurrentUser is not null || IsGuest;

    public async Task LoadAsync()
    {
        if (loaded) return;
        loaded = true;

        var stored = await storage.GetAsync<string>(Key);
        if (!stored.Success || stored.Value is null) return;

        if (stored.Value == "guest") { IsGuest = true; return; }

        if (stored.Value.StartsWith("user:") && int.TryParse(stored.Value[5..], out var id))
        {
            await using var db = await dbFactory.CreateDbContextAsync();
            CurrentUser = await db.Users.FindAsync(id);
        }
    }

    public async Task<(bool Ok, string? Error)> SignUpAsync(string fullName, string email, string password)
    {
        email = email.Trim().ToLowerInvariant();
        await using var db = await dbFactory.CreateDbContextAsync();

        if (await db.Users.AnyAsync(u => u.Email == email))
            return (false, "An account with that email already exists. Try logging in instead.");

        var user = new AppUser { FullName = fullName.Trim(), Email = email };
        user.PasswordHash = hasher.HashPassword(user, password);
        db.Users.Add(user);
        await db.SaveChangesAsync();

        await StartSessionAsync(user);
        return (true, null);
    }

    public async Task<(bool Ok, string? Error)> LoginAsync(string email, string password)
    {
        email = email.Trim().ToLowerInvariant();
        await using var db = await dbFactory.CreateDbContextAsync();

        var user = await db.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user is null ||
            hasher.VerifyHashedPassword(user, user.PasswordHash, password) == PasswordVerificationResult.Failed)
            return (false, "Incorrect email or password.");

        await StartSessionAsync(user);
        return (true, null);
    }

    public async Task ContinueAsGuestAsync()
    {
        CurrentUser = null;
        IsGuest = true;
        await storage.SetAsync(Key, "guest");
    }

    public async Task SignOutAsync()
    {
        CurrentUser = null;
        IsGuest = false;
        await storage.DeleteAsync(Key);
    }

    private async Task StartSessionAsync(AppUser user)
    {
        CurrentUser = user;
        IsGuest = false;
        await storage.SetAsync(Key, $"user:{user.Id}");
    }
}
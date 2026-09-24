using Microsoft.EntityFrameworkCore;
using MaisonFleurie.Data;
using MaisonFleurie.Services;
using MaisonFleurie.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddSingleton<CartService>(); // Register cart service here

builder.Services.AddDbContextFactory<AppDbContext>(options =>
    options.UseSqlite("Data Source=maisonfleurie.db"));
builder.Services.AddScoped<AuthService>();

var app = builder.Build();

// Create the database on first run
using (var db = app.Services.GetRequiredService<IDbContextFactory<AppDbContext>>().CreateDbContext())
{
    db.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
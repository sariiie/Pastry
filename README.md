# Maison Fleurie — Blazor Web App

A pixel-close Blazor (.NET 8, Blazor Web App / Interactive Server) rebuild of the
"Maison Fleurie" pastry-shop Figma design — three pages, shared header/footer,
and a working contact form.

## Pages

| Route         | File                          | Notes |
|---------------|-------------------------------|-------|
| `/`           | `Components/Pages/Home.razor` | Hero, quote, Signature Creations, story split, testimonials, CTA |
| `/menu`       | `Components/Pages/Menu.razor` | Interactive category pills filter product sections + special orders CTA |
| `/our-story`  | `Components/Pages/About.razor`| Story hero, traceability cards, boutique info + working contact form |

Shared chrome lives in `Components/Layout/SiteHeader.razor` and `SiteFooter.razor`,
and the product tile used across Home/Menu is `Components/Shared/ProductCard.razor`.

All styling is in `wwwroot/css/app.css`, driven by CSS custom properties at the
top of the file (`--color-*`, `--font-*`) so the palette/typography can be
retuned in one place. Fonts are Google Fonts **Playfair Display** (headings)
and **Jost** (body/UI), loaded in `Components/App.razor`.

Product/story photos currently point at `picsum.photos` placeholder URLs
(deterministic per-seed, so they won't change between reloads) — swap the
`ImageUrl` values in `Home.razor` / `Menu.razor` / `About.razor` for real
product photography before shipping.

## Run it

Requires the [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0).

```bash
cd MaisonFleurie
dotnet restore
dotnet run
```

Then open the URL printed in the console (e.g. `http://localhost:5080`).

For live reload while editing:

```bash
dotnet watch
```

## Structure

```
MaisonFleurie/
├── Components/
│   ├── App.razor              # root HTML document, fonts, css
│   ├── Routes.razor           # router
│   ├── _Imports.razor
│   ├── Layout/
│   │   ├── MainLayout.razor
│   │   ├── SiteHeader.razor
│   │   └── SiteFooter.razor
│   ├── Pages/
│   │   ├── Home.razor
│   │   ├── Menu.razor
│   │   ├── About.razor
│   │   └── Error.razor
│   └── Shared/
│       └── ProductCard.razor
├── wwwroot/
│   └── css/app.css
├── Program.cs
├── appsettings.json
└── MaisonFleurie.csproj
```

## Notes / next steps

- The "Add to Cart" buttons and cart badge in the header are wired up as UI
  only — hook `ProductCard`'s `OnAdd` callback up to a real cart service if
  you want persisted state.
- The contact form (`/our-story`) validates client-side with
  `DataAnnotationsValidator` and shows a success panel on submit; wire
  `HandleSubmit` in `About.razor` to an email/CRM service for production use.
- The menu category pills filter sections client-side via component state —
  no page reload.

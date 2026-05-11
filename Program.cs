<<<<<<< HEAD
using facturationA.Data;
using facturationA.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Blazor
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// EF Core — SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IProduitService, ProduitService>();
builder.Services.AddScoped<IFactureService, FactureService>();
// ou SQLite :
// options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<facturationA.Components.App>()
    .AddInteractiveServerRenderMode();

app.Run();
=======
using facturationApp.Components;

using FacturationApp.Models;

var client = new Client { Id = 1, Nom = "Ahmed" };

var produit1 = new Produit
{
    Id = 1,
    Nom = "Peinture",
    PrixHT = 100,
    TauxTVA = 0.19m
};

var ligne = new LigneFacture
{
    Produit = produit1,
    Quantite = 2
};

var facture = new Facture
{
    Id = 1,
    Client = client
};

facture.Lignes.Add(ligne);

Console.WriteLine("Total HT : " + facture.TotalHT);
Console.WriteLine("TVA : " + facture.TotalTVA);
Console.WriteLine("Total TTC : " + facture.TotalTTC);









var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
>>>>>>> b53ad5ad37452948838beaaa6285fffeeb34b40b

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

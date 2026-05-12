using facturationApp.Models;
using Microsoft.EntityFrameworkCore;

namespace facturationApp.Data
{
    public class DataSeeder
    {
        private readonly AppDbContext _context;

        public DataSeeder(AppDbContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            if (await _context.Clients.AnyAsync()) return;

            // Clients
            var clients = new List<Client>
            {
                new Client { Nom = "Société Alpha SARL", Email = "alpha@alpha.tn",
                    Telephone = "+216 71 000 001", Adresse = "Av. Habib Bourguiba, Tunis" },
                new Client { Nom = "Beta Informatique", Email = "beta@beta.tn",
                    Telephone = "+216 71 000 002", Adresse = "Rue de la Liberté, Sfax" },
                new Client { Nom = "Gamma Trading", Email = "gamma@gamma.tn",
                    Telephone = "+216 71 000 003", Adresse = "Zone Industrielle, Sousse" },
                new Client { Nom = "Delta Services", Email = "delta@delta.tn",
                    Telephone = "+216 71 000 004", Adresse = "Av. de Carthage, Tunis" },
            };
            _context.Clients.AddRange(clients);

            // Produits
            var produits = new List<Produit>
            {
                new Produit { Designation = "Ordinateur Portable HP",
                    PrixHT = 1200.000m, TauxTVA = 19 },
                new Produit { Designation = "Imprimante Laser",
                    PrixHT = 450.000m, TauxTVA = 19 },
                new Produit { Designation = "Clavier + Souris",
                    PrixHT = 85.000m, TauxTVA = 19 },
                new Produit { Designation = "Logiciel Comptabilité",
                    PrixHT = 600.000m, TauxTVA = 7 },
                new Produit { Designation = "Formation .NET",
                    PrixHT = 800.000m, TauxTVA = 0 },
                new Produit { Designation = "Maintenance Annuelle",
                    PrixHT = 300.000m, TauxTVA = 7 },
            };
            _context.Produits.AddRange(produits);
            await _context.SaveChangesAsync();

            // Factures
            var factures = new List<Facture>
            {
                new Facture
                {
                    Numero = "FAC-2026-0001",
                    DateFacture = new DateTime(2026, 1, 15),
                    ClientId = clients[0].Id,
                    TimbreFiscal = 1.000m,
                    Lignes = new List<LigneFacture>
                    {
                        new LigneFacture { ProduitId = produits[0].Id,
                            Quantite = 2, PrixUnitaireHT = 1200.000m, TauxTVA = 19 },
                        new LigneFacture { ProduitId = produits[2].Id,
                            Quantite = 2, PrixUnitaireHT = 85.000m, TauxTVA = 19 },
                    }
                },
                new Facture
                {
                    Numero = "FAC-2026-0002",
                    DateFacture = new DateTime(2026, 2, 10),
                    ClientId = clients[1].Id,
                    TimbreFiscal = 1.000m,
                    Lignes = new List<LigneFacture>
                    {
                        new LigneFacture { ProduitId = produits[3].Id,
                            Quantite = 1, PrixUnitaireHT = 600.000m, TauxTVA = 7 },
                        new LigneFacture { ProduitId = produits[5].Id,
                            Quantite = 1, PrixUnitaireHT = 300.000m, TauxTVA = 7 },
                    }
                },
                new Facture
                {
                    Numero = "FAC-2026-0003",
                    DateFacture = new DateTime(2026, 3, 5),
                    ClientId = clients[2].Id,
                    TimbreFiscal = 1.000m,
                    Lignes = new List<LigneFacture>
                    {
                        new LigneFacture { ProduitId = produits[4].Id,
                            Quantite = 3, PrixUnitaireHT = 800.000m, TauxTVA = 0 },
                    }
                },
                new Facture
                {
                    Numero = "FAC-2026-0004",
                    DateFacture = new DateTime(2026, 4, 20),
                    ClientId = clients[3].Id,
                    TimbreFiscal = 1.000m,
                    Lignes = new List<LigneFacture>
                    {
                        new LigneFacture { ProduitId = produits[1].Id,
                            Quantite = 1, PrixUnitaireHT = 450.000m, TauxTVA = 19 },
                        new LigneFacture { ProduitId = produits[0].Id,
                            Quantite = 1, PrixUnitaireHT = 1200.000m, TauxTVA = 19 },
                    }
                },
                new Facture
                {
                    Numero = "FAC-2026-0005",
                    DateFacture = new DateTime(2026, 5, 1),
                    ClientId = clients[0].Id,
                    TimbreFiscal = 1.000m,
                    Lignes = new List<LigneFacture>
                    {
                        new LigneFacture { ProduitId = produits[3].Id,
                            Quantite = 2, PrixUnitaireHT = 600.000m, TauxTVA = 7 },
                        new LigneFacture { ProduitId = produits[4].Id,
                            Quantite = 1, PrixUnitaireHT = 800.000m, TauxTVA = 0 },
                    }
                },
            };
            _context.Factures.AddRange(factures);
            await _context.SaveChangesAsync();
        }
    }
}
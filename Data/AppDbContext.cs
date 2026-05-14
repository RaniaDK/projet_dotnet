using facturationApp.Models;
using Microsoft.EntityFrameworkCore;

namespace facturationApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Produit> Produits { get; set; }
        public DbSet<Facture> Factures { get; set; }
        public DbSet<LigneFacture> LignesFacture { get; set; }
        public DbSet<Entreprise> Entreprises { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Facture → Client
            modelBuilder.Entity<Facture>()
                .HasOne(f => f.Client)
                .WithMany(c => c.Factures)
                .HasForeignKey(f => f.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            // LigneFacture → Facture
            modelBuilder.Entity<LigneFacture>()
                .HasOne(l => l.Facture)
                .WithMany(f => f.Lignes)
                .HasForeignKey(l => l.FactureId)
                .OnDelete(DeleteBehavior.Cascade);

            // LigneFacture → Produit
            modelBuilder.Entity<LigneFacture>()
                .HasOne(l => l.Produit)
                .WithMany(p => p.LignesFacture)
                .HasForeignKey(l => l.ProduitId)
                .OnDelete(DeleteBehavior.Restrict);

            // Numéro de facture unique
            modelBuilder.Entity<Facture>()
                .HasIndex(f => f.Numero)
                .IsUnique();

            // Précision décimales
            modelBuilder.Entity<Produit>()
                .Property(p => p.TauxTVA)
                .HasPrecision(5, 2);

            modelBuilder.Entity<LigneFacture>()
                .Property(l => l.TauxTVA)
                .HasPrecision(5, 2);

            modelBuilder.Entity<LigneFacture>()
                .Property(l => l.PrixUnitaireHT)
                .HasPrecision(10, 3);

            modelBuilder.Entity<Facture>()
                .Property(f => f.TimbreFiscal)
                .HasPrecision(10, 3);
        }
    }
}
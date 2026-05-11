using facturationA.Models;
using Microsoft.EntityFrameworkCore;

namespace facturationA.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Produit> Produits { get; set; }
        public DbSet<Facture> Factures { get; set; }
        public DbSet<LigneFacture> LignesFacture { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Facture → Client (cascade delete désactivé)
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
            modelBuilder.Entity<Produit>()
                .Property(p => p.TauxTVA)
                .HasPrecision(5, 2);
        }
    }
}
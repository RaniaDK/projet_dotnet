<<<<<<< HEAD
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace facturationA.Models
{
    public class LigneFacture
    {
        public int Id { get; set; }

        // FK Facture
        public int FactureId { get; set; }
        public Facture? Facture { get; set; }

        // FK Produit
        [Required(ErrorMessage = "Le produit est obligatoire")]
        public int ProduitId { get; set; }
        public Produit? Produit { get; set; }

        [Required]
        [Range(1, 10000, ErrorMessage = "Quantité invalide")]
        public int Quantite { get; set; }

        // Prix unitaire HT au moment de la vente (snapshot)
        [Column(TypeName = "decimal(10,3)")]
        public decimal PrixUnitaireHT { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal TauxTVA { get; set; }

        // Propriétés calculées
        [NotMapped]
        public decimal TotalHT => PrixUnitaireHT * Quantite;

        [NotMapped]
        public decimal TotalTVA => TotalHT * TauxTVA / 100;

        [NotMapped]
        public decimal TotalTTC => TotalHT + TotalTVA;
=======
namespace FacturationApp.Models
{
    public class LigneFacture
    {
        public Produit Produit { get; set; }
        public int Quantite { get; set; }

        public decimal TotalHT => Produit.PrixHT * Quantite;

        public decimal TVA => TotalHT * Produit.TauxTVA;

        public decimal TotalTTC => TotalHT + TVA;
>>>>>>> b53ad5ad37452948838beaaa6285fffeeb34b40b
    }
}
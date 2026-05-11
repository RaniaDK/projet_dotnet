<<<<<<< HEAD
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace facturationA.Models
=======
namespace FacturationApp.Models
>>>>>>> b53ad5ad37452948838beaaa6285fffeeb34b40b
{
    public class Produit
    {
        public int Id { get; set; }
<<<<<<< HEAD

        [Required(ErrorMessage = "La désignation est obligatoire")]
        [StringLength(150)]
        public string Designation { get; set; } = string.Empty;

        [Required]
        [Range(0.01, 99999.99, ErrorMessage = "Prix invalide")]
        [Column(TypeName = "decimal(10,3)")]
        public decimal PrixHT { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "Taux TVA invalide")]
        public decimal TauxTVA { get; set; } // ex: 19, 7, 0

        // Propriété calculée
        [NotMapped]
        public decimal PrixTTC => PrixHT * (1 + TauxTVA / 100);

        // Navigation
        public ICollection<LigneFacture> LignesFacture { get; set; } = new List<LigneFacture>();
=======
        public string Nom { get; set; }
        public decimal PrixHT { get; set; }
        public decimal TauxTVA { get; set; }
>>>>>>> b53ad5ad37452948838beaaa6285fffeeb34b40b
    }
}
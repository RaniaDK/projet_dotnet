using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace facturationApp.Models
{
    public class Facture
    {
        public int Id { get; set; }

        [Required]
        public string Numero { get; set; } = string.Empty;

        [Required]
        public DateTime DateFacture { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "Le client est obligatoire")]
        public int ClientId { get; set; }
        public Client? Client { get; set; }

        public ICollection<LigneFacture> Lignes { get; set; } = new List<LigneFacture>();

        [Column(TypeName = "decimal(10,3)")]
        public decimal TimbreFiscal { get; set; } = 1.000m;

        [NotMapped]
        public decimal TotalHT => Lignes.Sum(l => l.TotalHT);

        [NotMapped]
        public decimal TotalTVA => Lignes.Sum(l => l.TotalTVA);

        [NotMapped]
        public decimal TotalTTC => TotalHT + TotalTVA + TimbreFiscal;
    }
}
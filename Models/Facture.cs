using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace facturationA.Models
{
    public class Facture
    {
        public int Id { get; set; }

        [Required]
        public string Numero { get; set; } = string.Empty;

        [Required]
        public DateTime DateFacture { get; set; } = DateTime.Today;

        // FK Client
        [Required(ErrorMessage = "Le client est obligatoire")]
        public int ClientId { get; set; }
        public Client? Client { get; set; }

        public ICollection<LigneFacture> Lignes { get; set; } = new List<LigneFacture>();

        // Timbre fiscal tunisien (paramétrable)
        [Column(TypeName = "decimal(10,3)")]
        public decimal TimbreFiscal { get; set; } = 1.000m;

        // Propriétés calculées
        [NotMapped]
        public decimal TotalHT => Lignes.Sum(l => l.TotalHT);

        [NotMapped]
        public decimal TotalTVA => Lignes.Sum(l => l.TotalTVA);

        [NotMapped]
        public decimal TotalTTC => TotalHT + TotalTVA + TimbreFiscal;
    }
}
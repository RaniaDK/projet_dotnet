using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace facturationApp.Models
{
    public class Entreprise
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Le nom est obligatoire")]
        [StringLength(150)]
        public string Nom { get; set; } = string.Empty;

        [StringLength(250)]
        public string? Adresse { get; set; }

        [StringLength(30)]
        public string? Telephone { get; set; }

        [StringLength(30)]
        public string? MatriculeFiscal { get; set; }

        [StringLength(100)]
        public string? Email { get; set; }

        [StringLength(50)]
        public string? CodePostal { get; set; }

        [StringLength(50)]
        public string? Ville { get; set; }
    }
}
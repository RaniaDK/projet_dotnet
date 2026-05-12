using System.ComponentModel.DataAnnotations;

namespace facturationApp.Models
{
    public class Client
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Le nom est obligatoire")]
        [StringLength(100)]
        public string Nom { get; set; } = string.Empty;

        [Required(ErrorMessage = "L'email est obligatoire")]
        [EmailAddress(ErrorMessage = "Email invalide")]
        public string Email { get; set; } = string.Empty;

        [Phone]
        public string? Telephone { get; set; }

        [StringLength(200)]
        public string? Adresse { get; set; }

        public ICollection<Facture> Factures { get; set; } = new List<Facture>();
    }
}
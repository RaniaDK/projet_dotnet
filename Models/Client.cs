<<<<<<< HEAD
using System.ComponentModel.DataAnnotations;

namespace facturationA.Models
=======
using System.Collections.Generic;

namespace FacturationApp.Models
>>>>>>> b53ad5ad37452948838beaaa6285fffeeb34b40b
{
    public class Client
    {
        public int Id { get; set; }
<<<<<<< HEAD

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

        // Navigation
        public ICollection<Facture> Factures { get; set; } = new List<Facture>();
=======
        public string Nom { get; set; }
        public string Telephone { get; set; }

        // Relation : Client → Factures
        public List<Facture> Factures { get; set; } = new List<Facture>();
>>>>>>> b53ad5ad37452948838beaaa6285fffeeb34b40b
    }
}
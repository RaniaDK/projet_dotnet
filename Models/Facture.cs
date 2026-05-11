<<<<<<< HEAD
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace facturationA.Models
=======
using System;
using System.Collections.Generic;
using System.Linq;

namespace FacturationApp.Models
>>>>>>> b53ad5ad37452948838beaaa6285fffeeb34b40b
{
    public class Facture
    {
        public int Id { get; set; }
<<<<<<< HEAD

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
=======
        public DateTime Date { get; set; }

        public Client Client { get; set; }

        public List<LigneFacture> Lignes { get; set; } = new List<LigneFacture>();

        public decimal TotalHT => Lignes.Sum(l => l.TotalHT);

        public decimal TotalTVA => Lignes.Sum(l => l.TVA);

        public decimal TimbreFiscal { get; set; } = 1.000m;

>>>>>>> b53ad5ad37452948838beaaa6285fffeeb34b40b
        public decimal TotalTTC => TotalHT + TotalTVA + TimbreFiscal;
    }
}
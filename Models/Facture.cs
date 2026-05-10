using System;
using System.Collections.Generic;
using System.Linq;

namespace FacturationApp.Models
{
    public class Facture
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        public Client Client { get; set; }

        public List<LigneFacture> Lignes { get; set; } = new List<LigneFacture>();

        public decimal TotalHT => Lignes.Sum(l => l.TotalHT);

        public decimal TotalTVA => Lignes.Sum(l => l.TVA);

        public decimal TimbreFiscal { get; set; } = 1.000m;

        public decimal TotalTTC => TotalHT + TotalTVA + TimbreFiscal;
    }
}
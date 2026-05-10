using System.Collections.Generic;

namespace FacturationApp.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Telephone { get; set; }

        // Relation : Client → Factures
        public List<Facture> Factures { get; set; } = new List<Facture>();
    }
}
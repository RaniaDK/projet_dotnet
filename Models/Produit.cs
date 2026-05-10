namespace FacturationApp.Models
{
    public class Produit
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public decimal PrixHT { get; set; }
        public decimal TauxTVA { get; set; }
    }
}
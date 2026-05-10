namespace FacturationApp.Models
{
    public class LigneFacture
    {
        public Produit Produit { get; set; }
        public int Quantite { get; set; }

        public decimal TotalHT => Produit.PrixHT * Quantite;

        public decimal TVA => TotalHT * Produit.TauxTVA;

        public decimal TotalTTC => TotalHT + TVA;
    }
}
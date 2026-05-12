using facturationApp.Models;

namespace facturationApp.Services
{
    public interface IProduitService
    {
        Task<List<Produit>> GetAllAsync();
        Task<Produit?> GetByIdAsync(int id);
        Task<Produit> CreateAsync(Produit produit);
        Task<Produit> UpdateAsync(Produit produit);
        Task DeleteAsync(int id);
    }
}
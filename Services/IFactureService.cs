using facturationA.Models;

namespace facturationA.Services
{
    public interface IFactureService
    {
        Task<List<Facture>> GetAllAsync();
        Task<Facture?> GetByIdAsync(int id);
        Task<Facture> CreateAsync(Facture facture);
        Task<Facture> UpdateAsync(Facture facture);
        Task DeleteAsync(int id);
        Task<string> GenererNumeroAsync();
    }
}
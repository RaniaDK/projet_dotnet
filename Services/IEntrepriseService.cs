using facturationApp.Models;

namespace facturationApp.Services
{
    public interface IEntrepriseService
    {
        Task<Entreprise> GetAsync();
        Task SaveAsync(Entreprise entreprise);
    }
}
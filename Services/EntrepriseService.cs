using facturationApp.Data;
using facturationApp.Models;
using Microsoft.EntityFrameworkCore;

namespace facturationApp.Services
{
    public class EntrepriseService : IEntrepriseService
    {
        private readonly AppDbContext _context;

        public EntrepriseService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Entreprise> GetAsync()
        {
            var entreprise = await _context.Entreprises.FirstOrDefaultAsync();
            if (entreprise == null)
            {
                // Créer une entrée par défaut
                entreprise = new Entreprise
                {
                    Nom = "MON ENTREPRISE",
                    Adresse = "Tunis, Tunisie",
                    Telephone = "+216 XX XXX XXX",
                    MatriculeFiscal = "XXXXXXX/A/M/000",
                    Email = "contact@entreprise.tn",
                    Ville = "Tunis"
                };
                _context.Entreprises.Add(entreprise);
                await _context.SaveChangesAsync();
            }
            return entreprise;
        }

        public async Task SaveAsync(Entreprise entreprise)
        {
            var existing = await _context.Entreprises.FirstOrDefaultAsync();
            if (existing == null)
            {
                _context.Entreprises.Add(entreprise);
            }
            else
            {
                existing.Nom = entreprise.Nom;
                existing.Adresse = entreprise.Adresse;
                existing.Telephone = entreprise.Telephone;
                existing.MatriculeFiscal = entreprise.MatriculeFiscal;
                existing.Email = entreprise.Email;
                existing.CodePostal = entreprise.CodePostal;
                existing.Ville = entreprise.Ville;
            }
            await _context.SaveChangesAsync();
        }
    }
}
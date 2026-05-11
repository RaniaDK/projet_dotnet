using facturationA.Data;
using facturationA.Models;
using Microsoft.EntityFrameworkCore;

namespace facturationA.Services
{
    public class FactureService : IFactureService
    {
        private readonly AppDbContext _context;
        public FactureService(AppDbContext context) => _context = context;

        public async Task<List<Facture>> GetAllAsync()
            => await _context.Factures
                .Include(f => f.Client)
                .Include(f => f.Lignes).ThenInclude(l => l.Produit)
                .OrderByDescending(f => f.DateFacture)
                .ToListAsync();

        public async Task<Facture?> GetByIdAsync(int id)
            => await _context.Factures
                .Include(f => f.Client)
                .Include(f => f.Lignes).ThenInclude(l => l.Produit)
                .FirstOrDefaultAsync(f => f.Id == id);

        public async Task<Facture> CreateAsync(Facture facture)
        {
            _context.Factures.Add(facture);
            await _context.SaveChangesAsync();
            return facture;
        }

        public async Task<Facture> UpdateAsync(Facture facture)
        {
            _context.Factures.Update(facture);
            await _context.SaveChangesAsync();
            return facture;
        }

        public async Task DeleteAsync(int id)
        {
            var facture = await _context.Factures.FindAsync(id);
            if (facture != null)
            {
                _context.Factures.Remove(facture);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<string> GenererNumeroAsync()
        {
            var annee = DateTime.Today.Year;
            var count = await _context.Factures
                .CountAsync(f => f.DateFacture.Year == annee);
            return $"FAC-{annee}-{(count + 1):D4}";
        }
    }
}
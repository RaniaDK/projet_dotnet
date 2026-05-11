using facturationA.Data;
using facturationA.Models;
using Microsoft.EntityFrameworkCore;

namespace facturationA.Services
{
    public class ProduitService : IProduitService
    {
        private readonly AppDbContext _context;
        public ProduitService(AppDbContext context) => _context = context;

        public async Task<List<Produit>> GetAllAsync()
            => await _context.Produits.OrderBy(p => p.Designation).ToListAsync();

        public async Task<Produit?> GetByIdAsync(int id)
            => await _context.Produits.FindAsync(id);

        public async Task<Produit> CreateAsync(Produit produit)
        {
            _context.Produits.Add(produit);
            await _context.SaveChangesAsync();
            return produit;
        }

        public async Task<Produit> UpdateAsync(Produit produit)
        {
            _context.Produits.Update(produit);
            await _context.SaveChangesAsync();
            return produit;
        }

        public async Task DeleteAsync(int id)
        {
            var produit = await _context.Produits.FindAsync(id);
            if (produit != null)
            {
                _context.Produits.Remove(produit);
                await _context.SaveChangesAsync();
            }
        }
    }
}
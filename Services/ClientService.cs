using facturationApp.Data;
using facturationApp.Models;
using Microsoft.EntityFrameworkCore;

namespace facturationApp.Services
{
    public class ClientService : IClientService
    {
        private readonly AppDbContext _context;

        public ClientService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Client>> GetAllAsync()
            => await _context.Clients.OrderBy(c => c.Nom).ToListAsync();

        public async Task<Client?> GetByIdAsync(int id)
            => await _context.Clients.Include(c => c.Factures).FirstOrDefaultAsync(c => c.Id == id);

        public async Task<Client> CreateAsync(Client client)
        {
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
            return client;
        }

        public async Task<Client> UpdateAsync(Client client)
        {
            _context.Clients.Update(client);
            await _context.SaveChangesAsync();
            return client;
        }

        public async Task DeleteAsync(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client != null)
            {
                _context.Clients.Remove(client);
                await _context.SaveChangesAsync();
            }
        }
    }
}
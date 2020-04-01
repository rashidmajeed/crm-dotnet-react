using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private DataContext _context;

        public ClientRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Client> Add(Client client)
        {
            await _context.Clients.AddAsync(client);
            await _context.SaveChangesAsync();
            return client;
        }

        public async Task<bool> Exist(int id)
        {
            return await _context.Clients.AnyAsync(c => c.Id == id);
        }

        public async Task<Client> Find(int id)
        {
            return await _context.Clients.Include(client => client.Order).SingleOrDefaultAsync(a => a.Id == id);
        }

        public IEnumerable<Client> GetAll()
        {
            return _context.Clients;
        }

        public async Task<Client> Remove(int id)
        {
            var client = await _context.Clients.SingleAsync(a => a.Id == id);
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
            return client;
        }

        public async Task<Client> Update(Client client)
        {
            _context.Clients.Update(client);
            await _context.SaveChangesAsync();
            return client;
        }

        public Task Save()
        {
           return _context.SaveChangesAsync();
        }
    }
}


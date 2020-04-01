using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private DataContext _context;

        public OrderRepository(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<Order> GetAll()
        {
            return _context.Orders;
        }

        public async Task<Order> Add(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<Order> Find(int id)
        {
            return await _context.Orders.SingleOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Order> Remove(int id)
        {
            var order = _context.Orders.Single(a => a.Id == id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<Order> Update(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<bool> Exists(int id)
        {
            return await _context.Orders.AnyAsync(e => e.Id == id);
        }
        public Task Save()
        {
           return _context.SaveChangesAsync();
        }
    }
}
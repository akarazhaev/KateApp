using DIshesApp.Data.Interfaces;
using DIshesApp.Entity;
using Microsoft.EntityFrameworkCore;

namespace DIshesApp.Data.Implements
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetAll() =>
            await _context.Orders.ToListAsync();

        public async Task<Order> GetById(int id) =>
            await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<bool> Create(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(Order order)
        {
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

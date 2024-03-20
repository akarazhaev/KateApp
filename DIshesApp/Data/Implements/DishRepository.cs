using DIshesApp.Data.Interfaces;
using DIshesApp.Entity;
using Microsoft.EntityFrameworkCore;

namespace DIshesApp.Data.Implements
{
    public class DishRepository : IDishRepository
    {
        private readonly ApplicationDbContext _context;

        public DishRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Dish>> GetAll() =>
            await _context.Dishes.ToListAsync();

        public async Task<Dish> GetById(int id) =>
            await _context.Dishes.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<bool> Create(Dish dish)
        {
            _context.Dishes.Add(dish);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Update(Dish dish)
        {
            _context.Dishes.Update(dish);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(Dish dish)
        {
            _context.Dishes.Remove(dish);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}

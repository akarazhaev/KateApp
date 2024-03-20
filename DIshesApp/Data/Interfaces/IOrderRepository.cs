using DIshesApp.Entity;

namespace DIshesApp.Data.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order> GetById(int id);
        Task<bool> Create(Order order);
        Task<bool> Update(Order order);
        Task<bool> Delete(Order order);
    }
}

using DIshesApp.DTOs;
using System.ComponentModel.DataAnnotations.Schema;

namespace DIshesApp.Entity
{
    public class Order
    {
        public int Id { get; set; }
        public bool IsReady { get; set; }
        [NotMapped]
        public IEnumerable<DishDto> Dishes { get; set; }
    }
}

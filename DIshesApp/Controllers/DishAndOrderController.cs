using Azure;
using DIshesApp.Data.Interfaces;
using DIshesApp.DTOs;
using DIshesApp.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace DIshesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DishAndOrderController : ControllerBase
    {
        private readonly IDishRepository _dishRepository;
        private readonly IOrderRepository _orderRepository;

        public DishAndOrderController(IDishRepository dishRepository, IOrderRepository orderRepository)
        {
            _dishRepository = dishRepository;
            _orderRepository = orderRepository;
        }

        //Dishes
        [HttpGet("dishes/{id}")]
        public async Task<IActionResult> GetDishById(int id)
        {
            var dish = await _dishRepository.GetById(id);
            if (dish == null)
            {
                return NotFound("Dish not founded");
            }

            DishDto output = new DishDto()
            {
                Name = dish.Name,
                Weight = dish.Weight.ToString(),
                Price = dish.Price.ToString(),
                ImageUrl = dish.ImageUrl
            };

            return Ok(output);
        }

        [HttpPost("dishes")]
        public async Task<IActionResult> CreateDish(Dish request)
        {
            Dish dish = new Dish()
            {
                Id = request.Id,
                Name = request.Name,
                Weight = request.Weight,
                Price = request.Price,
                ImageUrl = request.ImageUrl
            };

            var status = await _dishRepository.Create(dish);
            if (status != true)
            {
                return BadRequest("Invalid body request");
            }
            return Ok("Created");
        }

        // Orders
        [HttpGet("orders/{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _orderRepository.GetById(id);
            return Ok(order);
        }

        [HttpPost("orders")]
        public async Task<IActionResult> CreateOrder(Order request)
        {
            Order order = new Order()
            {
                Id = request.Id,
                IsReady = false,
                Dishes = request.Dishes
            };
            
            var status = await _orderRepository.Create(order);
            if (status != true)
            {
                return BadRequest("Invalid body request");
            }

            return Ok("Created");
        }

        [HttpPatch("orders/{id}")]
        public async Task<IActionResult> UpdateOrder(int id,[FromBody] JsonPatchDocument<Order> patchDocument)
        {

            var order = await _orderRepository.GetById(id);

            patchDocument.ApplyTo(order, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _orderRepository.Update(order);
            return NoContent();
        }

        [HttpDelete("orders/{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _orderRepository.GetById(id);

            if (order.IsReady != true)
            {
                return BadRequest("The order is not ready");
            }
            await _orderRepository.Delete(order);
            return Ok("deleted");
        }
    }
}

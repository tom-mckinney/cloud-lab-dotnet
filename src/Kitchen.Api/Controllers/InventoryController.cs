using System.Collections.Generic;
using System.Threading.Tasks;
using Kitchen.Api.Models;
using Kitchen.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kitchen.Api.Controllers
{
    [Route("api/[controller]")]
    public class InventoryController : Controller
    {
        private readonly IInventoryRepository _orderRepository;

        public InventoryController(IInventoryRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet]
        public Task<List<Cupcake>> Index()
        {
            return _orderRepository.GetAllAsync();
        }

        [HttpGet("{id}")]
        public Task<Cupcake> GetById(int id)
        {
            return _orderRepository.GetByIdAsync(id);
        }

        [HttpPost]
        public Task AddToInventory([FromBody] Cupcake cupcake)
        {
            return _orderRepository.AddAsync(cupcake);
        }

        [HttpPut("{id}")]
        public Task UpdateProduct([FromRoute] int id, [FromBody] Cupcake cupcake)
        {
            return _orderRepository.UpdateAsync(id, cupcake);
        }
    }
}

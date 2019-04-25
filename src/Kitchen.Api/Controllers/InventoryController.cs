using System.Collections.Generic;
using System.Threading.Tasks;
using Kitchen.Api.Models;
using Kitchen.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kitchen.Api.Controllers
{
    public class InventoryController : Controller
    {
        private readonly IInventoryRepository _inventoryRepository;

        public InventoryController(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        [HttpGet]
        public Task<List<Cupcake>> Index()
        {
            return _inventoryRepository.GetAllAsync();
        }
    }
}

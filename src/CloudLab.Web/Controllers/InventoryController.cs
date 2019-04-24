using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CloudLab.Web.Models;
using CloudLab.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudLab.Web.Controllers
{
    public class InventoryController : Controller
    {
        private readonly IInventoryRepository _inventoryRepository;

        public InventoryController(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IList<Cupcake> cupcakes = await _inventoryRepository.GetAllAsync();

            return Ok(cupcakes);
        }
    }
}

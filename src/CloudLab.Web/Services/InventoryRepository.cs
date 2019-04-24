using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudLab.Web.Data;
using CloudLab.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace CloudLab.Web.Services
{
    public interface IInventoryRepository
    {
        Task<List<Cupcake>> GetAllAsync();
    }

    public class InventoryRepository : IInventoryRepository
    {
        private readonly ApplicationDbContext _context;

        public InventoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<List<Cupcake>> GetAllAsync()
        {
            return _context.Cupcakes.AsNoTracking().ToListAsync();
        }
    }
}

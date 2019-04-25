using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kitchen.Api.Data;
using Kitchen.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Kitchen.Api.Services
{
    public interface IInventoryRepository
    {
        Task<List<Cupcake>> GetAllAsync();
        Task AddAsync(Cupcake cupcake);
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

        public async Task AddAsync(Cupcake cupcake)
        {
            if (cupcake == null)
            {
                throw new ArgumentException("Cupcake cannot be null", nameof(cupcake));
            }

            _context.Cupcakes.Add(cupcake);

            await _context.SaveChangesAsync();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kitchen.Api.Data;
using Kitchen.Api.Exceptions;
using Kitchen.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Kitchen.Api.Services
{
    public interface IInventoryRepository
    {
        Task AddAsync(Cupcake cupcake);
        Task<List<Cupcake>> GetAllAsync();
        Task<Cupcake> GetByIdAsync(int id);
        Task UpdateAsync(int id, Cupcake cupcake);
    }

    public class InventoryRepository : IInventoryRepository
    {
        private readonly ApplicationDbContext _context;

        public InventoryRepository(ApplicationDbContext context)
        {
            _context = context;
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

        public Task<List<Cupcake>> GetAllAsync()
        {
            return _context.Cupcakes.AsNoTracking().ToListAsync();
        }

        public async Task<Cupcake> GetByIdAsync(int id)
        {
            Cupcake cupcake = await _context.Cupcakes
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cupcake == null)
                throw new ProductNotFoundException();

            return cupcake;
        }

        public async Task UpdateAsync(int id, Cupcake cupcake)
        {
            Cupcake existingCupcake = await GetByIdAsync(id);

            existingCupcake.Name = cupcake.Name;

            _context.Cupcakes.Update(existingCupcake);

            await _context.SaveChangesAsync();
        }
    }
}

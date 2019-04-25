using Kitchen.Api.Data;
using Kitchen.Api.Models;
using Kitchen.Api.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Kitchen.Test.Services
{
    public class InventoryRepositoryTests
    {
        private readonly ApplicationDbContext _context;

        public InventoryRepositoryTests()
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("inventory_db");

            _context = new ApplicationDbContext(builder.Options);
        }

        [Fact]
        public async Task GetAllAsync_returns_all_cupcakes_in_repository()
        {
            _context.Cupcakes.Add(new Cupcake { Name = "Delicious Cupcake" });
            _context.Cupcakes.Add(new Cupcake { Name = "Okay Cupcake" });
            _context.Cupcakes.Add(new Cupcake { Name = "Terrible Cupcake" });

            _context.SaveChanges();

            IInventoryRepository repository = new InventoryRepository(_context);

            var allCupcakes = await repository.GetAllAsync();

            Assert.Equal(3, allCupcakes.Count);
        }

        [Fact]
        public async Task AddAsync_inserts_model_into_repository()
        {
            IInventoryRepository repository = new InventoryRepository(_context);

            Cupcake cupcake = new Cupcake { Name = "FooBar Sprinkles" };

            await repository.AddAsync(cupcake);

            var savedCupcake = Assert.Single(_context.Cupcakes);

            Assert.Equal(cupcake.Name, savedCupcake.Name);
        }

        [Fact]
        public void AddAsync_throws_if_model_is_null()
        {
            IInventoryRepository repository = new InventoryRepository(_context);


            Cupcake cupcake = null;

            Assert.ThrowsAsync<ArgumentException>(() => repository.AddAsync(cupcake));
        }
    }
}

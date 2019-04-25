using Kitchen.Api.Data;
using Kitchen.Api.Exceptions;
using Kitchen.Api.Models;
using Kitchen.Api.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Net;
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
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            _context = new ApplicationDbContext(builder.Options);
        }

        private IInventoryRepository CreateService()
        {
            return new InventoryRepository(_context);
        }

        [Fact]
        public async Task GetAllAsync_returns_all_cupcakes_in_repository()
        {
            _context.Cupcakes.Add(new Cupcake { Name = "Delicious Cupcake" });
            _context.Cupcakes.Add(new Cupcake { Name = "Okay Cupcake" });
            _context.Cupcakes.Add(new Cupcake { Name = "Terrible Cupcake" });

            _context.SaveChanges();

            IInventoryRepository repository = CreateService();

            var allCupcakes = await repository.GetAllAsync();

            Assert.Equal(3, allCupcakes.Count);
        }

        [Fact]
        public async Task GetByIdAsync_returns_cupcake_with_matching_id()
        {
            _context.Cupcakes.Add(new Cupcake { Id = 1, Name = "First Cupcake" });
            _context.Cupcakes.Add(new Cupcake { Id = 2, Name = "Second Cupcake" });

            _context.SaveChanges();

            IInventoryRepository repository = CreateService();

            var secondCupcake = await repository.GetByIdAsync(2);

            Assert.Equal(2, secondCupcake.Id);
            Assert.Equal("Second Cupcake", secondCupcake.Name);
        }

        [Fact]
        public async Task GetByIdAsync_throws_if_there_is_no_cupcake_with_matching_id()
        {
            _context.Cupcakes.Add(new Cupcake { Id = 1, Name = "Highlander Cupcake" });

            _context.SaveChanges();

            IInventoryRepository repository = CreateService();

            var ex = await Assert.ThrowsAsync<ProductNotFoundException>(() => repository.GetByIdAsync(999));
            Assert.Equal(HttpStatusCode.NotFound, ex.Status);
        }

        [Fact]
        public async Task AddAsync_inserts_model_into_repository()
        {
            IInventoryRepository repository = CreateService();

            Cupcake cupcake = new Cupcake { Name = "FooBar Sprinkles" };

            await repository.AddAsync(cupcake);

            var savedCupcake = Assert.Single(_context.Cupcakes);

            Assert.Equal(cupcake.Name, savedCupcake.Name);
        }

        [Fact]
        public void AddAsync_throws_if_model_is_null()
        {
            IInventoryRepository repository = CreateService();


            Cupcake cupcake = null;

            Assert.ThrowsAsync<ArgumentException>(() => repository.AddAsync(cupcake));
        }
    }
}

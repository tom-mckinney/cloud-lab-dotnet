using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudLab.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace CloudLab.Web.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<DbContext> options) : base(options) { }

        public DbSet<Cupcake> Cupcakes { get; set; }
    }
}

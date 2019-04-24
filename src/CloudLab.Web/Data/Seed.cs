using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudLab.Web.Models;

namespace CloudLab.Web.Data
{
    public static class Seed
    {
        public static void SeedData(ApplicationDbContext context)
        {
            context.Cupcakes.Add(new Cupcake
            {
                Name = "Boston Cream",

            });

            context.Cupcakes.Add(new Cupcake
            {
                Name = "Strawberry Frosting",

            });

            context.Cupcakes.Add(new Cupcake
            {
                Name = "Chocolate",
            });

            context.Cupcakes.Add(new Cupcake
            {
                Name = "Old Fashion",
            });

            context.SaveChanges();
        }
    }
}

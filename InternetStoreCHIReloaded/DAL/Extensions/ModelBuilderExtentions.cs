using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Extensions
{
    public static class ModelBuilderExtentions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var timeNow = DateTime.UtcNow;

            modelBuilder.Entity<CategoryEntity>().HasData(
                new CategoryEntity()
                {
                    Id = -1,
                    Name = "Smatrphone",
                    Description = "Like the phone, but cooler",
                    CreatedDate = timeNow,
                    UpdatedDate = timeNow
                },
                new CategoryEntity()
                {
                    Id = -2,
                    Name = "TV",
                    Description = "Like the smartphone, but bigger",
                    CreatedDate = timeNow,
                    UpdatedDate = timeNow
                },
                new CategoryEntity()
                {
                    Id = -3,
                    Name = "Car",
                    Description = "Totally not like a TV, gives you a nice ride though",
                    CreatedDate = timeNow,
                    UpdatedDate = timeNow
                });

            modelBuilder.Entity<ProductEntity>().HasData(
                new ProductEntity()
                {
                    Id = -1,
                    Name = "Shwamsung Galaxy Milky Way 8 - Infinitely Blue",
                    Description = "Best smartphone for the best daddies",
                    Price = (decimal)1337.69,
                    CreatedDate = timeNow,
                    UpdatedDate = timeNow
                },
                new ProductEntity()
                {
                    Id = -2,
                    Name = "Salami Mi LED TV 4S 50 inch",
                    Description = "4k to masses, money in cases",
                    Price = (decimal)484.74,
                    CreatedDate = timeNow,
                    UpdatedDate = timeNow
                },
                new ProductEntity()
                {
                    Id = -3,
                    Name = "Edison Model S",
                    Description = "DC Powah!",
                    Price = (decimal)123000.0,
                    CreatedDate = timeNow,
                    UpdatedDate = timeNow
                });
        }
    }
}

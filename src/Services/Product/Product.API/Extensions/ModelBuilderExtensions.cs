using Microsoft.EntityFrameworkCore;
using Product.API.Entities;

public static class ModelBuilderExtensions
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CatalogProduct>().HasData(
            new()
            {
                Id = 1,
                No = "Latus",
                Name = "Esprit",
                Summanry = "Summanry1",
                Description = "Neque porro quisquam est qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit",
                Price = (decimal)17789.23
            },
            new()
            {
                Id=2,
                No = "Cadillac",
                Name = "CTS",
                Summanry = "Summanry2",
                Description = "Neque porro quisquam est qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit",
                Price = (decimal)2332.23
            }
        );
  
    }
}
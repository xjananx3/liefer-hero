using LieferHero.Models;

namespace LieferHero.Data;

public class Seed
{
    public static void SeedData(IApplicationBuilder applicationBuilder)
    {
        using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
        {
            var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

            context.Database.EnsureCreated();

            if (!context.Speisen.Any())
            {
                context.Speisen.AddRange(new List<Speise>()
                {
                    new Speise()
                    {
                        Name = "Classic Cheese Burger",
                        Price = 9,
                        ErstelltAm = new DateTime(2024, 7, 12)
                    },
                    new Speise()
                    {
                        Name = "Homestyle Double Smash",
                        Price = 11.5m,
                        ErstelltAm = new DateTime(2024, 7, 12)
                    },
                    new Speise()
                    {
                        Name = "Spicy Chicken Sandwich",
                        Price = 8.5m,
                        ErstelltAm = new DateTime(2024, 7, 12)
                    },
                    new Speise()
                    {
                        Name = "Chili Cheese Fries",
                        Price = 5.5m,
                        ErstelltAm = new DateTime(2024, 7, 12)
                    },
                    new Speise()
                    {
                        Name = "Süßkartoffel Fries",
                        Price = 4,
                        ErstelltAm = new DateTime(2024, 7, 12)
                    },
                    new Speise()
                    {
                        Name = "BBQ Bacon Burger",
                        Price = 10,
                        ErstelltAm = new DateTime(2024, 7, 12)
                    },
                    new Speise()
                    {
                        Name = "Veggie Delight Burger",
                        Price = 9,
                        ErstelltAm = new DateTime(2024, 7, 12)
                    },
                    new Speise()
                    {
                        Name = "Crispy Chicken Wrap",
                        Price = 7.5m,
                        ErstelltAm = new DateTime(2024, 7, 12)
                    },
                    new Speise()
                    {
                        Name = "Caesar Salad",
                        Price = 6,
                        ErstelltAm = new DateTime(2024, 7, 12)
                    },
                    new Speise()
                    {
                        Name = "Grilled Cheese Sandwich",
                        Price = 5,
                        ErstelltAm = new DateTime(2024, 7, 12)
                    },
                    new Speise()
                    {
                        Name = "Avocado Toast",
                        Price = 6.5m,
                        ErstelltAm = new DateTime(2024, 7, 12)
                    },
                    new Speise()
                    {
                        Name = "Pulled Pork Sandwich",
                        Price = 9.5m,
                        ErstelltAm = new DateTime(2024, 7, 12)
                    }
                });
                context.SaveChanges();
            }
        }
    }
}
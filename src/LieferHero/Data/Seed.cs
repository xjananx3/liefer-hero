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
                        Preis = 9,
                        ErstelltAm = new DateTime(2024, 7, 12)
                    },
                    new Speise()
                    {
                        Name = "Homestyle Double Smash",
                        Preis = 11.5m,
                        ErstelltAm = new DateTime(2024, 7, 12)
                    },
                    new Speise()
                    {
                        Name = "Spicy Chicken Sandwich",
                        Preis = 8.5m,
                        ErstelltAm = new DateTime(2024, 7, 12)
                    },
                    new Speise()
                    {
                        Name = "Chili Cheese Fries",
                        Preis = 5.5m,
                        ErstelltAm = new DateTime(2024, 7, 12)
                    },
                    new Speise()
                    {
                        Name = "Süßkartoffel Fries",
                        Preis = 4,
                        ErstelltAm = new DateTime(2024, 7, 12)
                    },
                    new Speise()
                    {
                        Name = "BBQ Bacon Burger",
                        Preis = 10,
                        ErstelltAm = new DateTime(2024, 7, 12)
                    },
                    new Speise()
                    {
                        Name = "Veggie Delight Burger",
                        Preis = 9,
                        ErstelltAm = new DateTime(2024, 7, 12)
                    },
                    new Speise()
                    {
                        Name = "Crispy Chicken Wrap",
                        Preis = 7.5m,
                        ErstelltAm = new DateTime(2024, 7, 12)
                    },
                    new Speise()
                    {
                        Name = "Caesar Salad",
                        Preis = 6,
                        ErstelltAm = new DateTime(2024, 7, 12)
                    },
                    new Speise()
                    {
                        Name = "Grilled Cheese Sandwich",
                        Preis = 5,
                        ErstelltAm = new DateTime(2024, 7, 12)
                    },
                    new Speise()
                    {
                        Name = "Avocado Toast",
                        Preis = 6.5m,
                        ErstelltAm = new DateTime(2024, 7, 12)
                    },
                    new Speise()
                    {
                        Name = "Pulled Pork Sandwich",
                        Preis = 9.5m,
                        ErstelltAm = new DateTime(2024, 7, 12)
                    }
                });
                context.SaveChanges();
            }
        }
    }
}
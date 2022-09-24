using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat.Data
{
    public class DutchSeeder
    {
        private readonly DutchContext _context;
        private readonly IHostingEnvironment _environment;
        private readonly UserManager<StoreUser> _userManager;

        public DutchSeeder(UserManager<StoreUser> userManager, DutchContext context, IHostingEnvironment environment)
        {
            _userManager = userManager;
            _context = context;
            _environment = environment;
            
        }

        public async Task SeedAsync()
        {
            _context.Database.EnsureCreated();
            StoreUser user = await _userManager.FindByEmailAsync("Tahsin058@live.nl");

            if (user == null)
            {
                user = new StoreUser()
                {
                    FirstName = "Tahsin",
                    LastName = "Kaya",
                    Email = "Tahsin058@live.nl",
                    UserName = "Tahsin058@live.nl"
                };
                var result = await _userManager.CreateAsync(user, "P@ssw0rd!");

                if(result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create new user in seeder");
                }
            }

            if (!_context.Products.Any())
            {
                var filePath = Path.Combine(_environment.ContentRootPath,"Data/art.json");
                var json = File.ReadAllText(filePath);
                var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(json);

                _context.Products.AddRange(products);


                var order = _context.Orders.Where(o => o.Id == 1).FirstOrDefault();
                if(order != null)
                {
                    order.User = user;
                    order.Items = new List<OrderItem>()
                    {
                        new OrderItem()
                        {
                            Product=products.First(),
                            Quantity=5,
                            UnitPrice=products.First().Price
                        }
                    };
                }
                _context.Orders.Add(order);
                _context.SaveChanges();
                /*
                var order = new Order()
                {
                    OrderDate = DateTime.Today,
                    OrderNumber = "10000",
                    Items = new List<OrderItem>()
                    {
                        new OrderItem()
                        {
                            Product=products.First(),
                            Quantity=5,
                            UnitPrice=products.First().Price
                        }
                    }
                };
                */
            }
        }
    }
}

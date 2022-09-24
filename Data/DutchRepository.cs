using DutchTreat.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat.Data
{
    public class DutchRepository : IDutchRepository
    {
        private readonly DutchContext _context;
        private readonly ILogger<DutchRepository> _logger;
        public DutchRepository(DutchContext context, ILogger<DutchRepository> logger)
        {
            this._context = context;
            this._logger = logger;
        }

        public void AddEntity(object model)
        {
            _context.Add(model);
        }

        public IEnumerable<Order> GetAllOrders(bool includeItems)
        {
            if (includeItems)
            {
                return _context.Orders.Include(o => o.Items)
                    .ThenInclude(o => o.Product)
                    .ToList();
            }
            else
            {
                return _context.Orders.ToList();
            }
        }

        public IEnumerable<Product> GetAllProducts()
        {
            try
            {
                _logger.LogInformation("Get all products were called");
                return _context.Products.OrderBy(p => p.Title).ToList();
            }
            catch(Exception ex)
            {
                //not been enabled in config.json
                _logger.LogError($"failed to get all products{ex}");
                return null;
            }
        }

        public Order GetOrderById(int id)
        {
            return _context.Orders
                .Include(o=>o.Items)
                .ThenInclude(i=>i.Product)
                .Where(o => o.Id == id)
                .FirstOrDefault();
        }
       

        public IEnumerable<Product> GetProductByCategory(string category)
        {
            return _context.Products.Where(p => p.Category == category).
                 OrderBy(c => c).ToList();
        }

        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }
    }
}

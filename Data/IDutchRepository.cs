using DutchTreat.Data.Entities;
using System.Collections.Generic;

namespace DutchTreat.Data
{
    public interface IDutchRepository
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetProductByCategory(string category);
        
        IEnumerable<Order> GetAllOrders(bool items);
        Order GetOrderById(int id);

        bool SaveAll();
        void AddEntity(object model);
    }
}
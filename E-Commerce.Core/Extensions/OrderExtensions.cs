using E_Commerce.Core.Entitiies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Extensions
{
    public static class OrderExtensions
    {
        public static decimal CalculateTotalAmount(this Order order)
        {
            return order.Products.Sum(p => p.Price * p.Quantity);
        }
    }
}

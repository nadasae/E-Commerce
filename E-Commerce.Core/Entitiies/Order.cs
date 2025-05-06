using E_Commerce.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Entitiies
{
    public class Order : Entity<int>
    {
        public DateTime OrderDate { get; set; }
        public OrderStatus Status { get; set; } 
       // public decimal TotalAmount {  get; set; }
           
        public ICollection<Product> Products { get; set; } 
    }
}

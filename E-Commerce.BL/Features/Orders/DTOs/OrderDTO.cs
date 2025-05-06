using E_Commerce.BL.Features.Products.DTOs;
using E_Commerce.Core.Entitiies;
using E_Commerce.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.BL.Features.Orders.DTOs
{
    public class OrderDTO
    {
      
        public OrderStatus Status { get; set; }

        public List<ProductDTO> Products { get; set; } = new();
        public decimal Total { get; set; }
    }
}

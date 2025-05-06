using E_Commerce.BL.Features.Orders.DTOs;
using E_Commerce.Core.Entitiies;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.BL.Mapping
{
    public class MappingConfig
    {
           public void Configure()
        {
            TypeAdapterConfig<Order, OrderDTO>.NewConfig();
        }
    }
}

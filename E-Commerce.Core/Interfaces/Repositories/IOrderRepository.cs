using E_Commerce.Core.Entitiies;
using E_Commerce.Core.Enums;
using E_Commerce.Core.Interfaces.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Interfaces.Repositories
{
    public interface IOrderRepository : IBaseRepository<Order,int>
    {
        Task<IEnumerable<Order>> FilterByStatusAsync(OrderStatus status);
    }
}

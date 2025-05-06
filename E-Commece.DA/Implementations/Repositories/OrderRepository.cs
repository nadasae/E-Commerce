using E_Commece.DA.Context;
using E_Commece.DA.Implementations.Base;
using E_Commerce.Core.Entitiies;
using E_Commerce.Core.Enums;
using E_Commerce.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commece.DA.Implementations.Repositories
{
    public class OrderRepository : BaseRepository<Order,int>, IOrderRepository
    {
        private readonly DbContext context;

        public OrderRepository(AppDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Order>> FilterByStatusAsync(OrderStatus status)
        {
            return await context.Set<Order>()
                                 .Where(o => o.Status == status)
                                 .ToListAsync();
        }
    }
}

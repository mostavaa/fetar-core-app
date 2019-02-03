using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Data.Repositories
{
    public class OrderDetailsRepository : Repository<OrderDetails>
    {
        public OrderDetailsRepository(DataContext context) : base(context)
        {

        }

        public OrderDetails GetById(int id)
        {
            return Get(o => o.Id == id).FirstOrDefault();
        }
        public void Create(OrderDetails item)
        {
            Add(item);
        }
        public void Edit(OrderDetails item)
        {
            Update(item);
        }

        public OrderDetails FindOrderDetailsForUser(int userId, int orderId)
        {
            return Get(o => o.UserId == userId && o.OrderId == orderId).Include(o=>o.ItemDetails).ThenInclude(o=>o.Item).FirstOrDefault();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace Data.Repositories
{
    public class OrderRepository : Repository<Order>
    {
        public OrderRepository(DataContext context) : base(context)
        {
        }
        public List<Order> GetAll(int page, int take)
        {
            return Get().OrderByDescending(o => o.CreationDate).Skip(page * take).Take(take).ToList();
        }
        public bool OrderCreatedToday()
        {
            var today = DateTime.Now.Date;
            return Get(o => o.CreationDate.Date == today).Any();
        }

        public void CreateOrderToday(Order model)
        {
            Add(model);
        }

        public int NumOfOrders()
        {
            return Get().Count();
        }

        public Order GetByGuid(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}

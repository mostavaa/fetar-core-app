using Microsoft.EntityFrameworkCore;
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

        public Order FindTodayOrder()
        {
            var today = DateTime.Now.Date;
            return Get(o => o.CreationDate.Date == today).FirstOrDefault();
        }

        public void CreateOrderToday(Order model)
        {
            if (model.GUID == Guid.Empty)
                model.GUID = Guid.NewGuid();
            Add(model);
        }

        public int NumOfOrders()
        {
            return Get().Count();
        }

        public Order FullGetByGuid(Guid id)
        {
            return Get(o => o.GUID == id).Include(o => o.OrderDetails).ThenInclude(o => o.User).Include(o => o.OrderDetails).ThenInclude(o => o.ItemDetails).ThenInclude(o => o.Item).FirstOrDefault();
        }

        public Order GetById(int id)
        {
            return Get(o => o.Id == id).FirstOrDefault();
        }

        public void Edit(Order item)
        {
            Update(item);
        }
    }
}

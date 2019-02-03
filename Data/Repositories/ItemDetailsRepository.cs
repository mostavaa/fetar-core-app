using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Data.Repositories
{
    public class ItemDetailsRepository : Repository<ItemDetails>
    {
        public ItemDetailsRepository(DataContext context) : base(context)
        {
        }

        public void Create(ItemDetails itemDetails)
        {
            Add(itemDetails);
        }

        public ItemDetails GetById(Guid id)
        {
            return Get(o => o.GUID == id).Include(o=>o.OrderDetails).ThenInclude(o=>o.Order).FirstOrDefault();
        }

        public void Remove(int id)
        {
            Delete(id);
        }
    }
}

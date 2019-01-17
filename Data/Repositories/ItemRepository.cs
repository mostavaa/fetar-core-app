using System;
using System.Collections.Generic;
using System.Linq;

namespace Data.Repositories
{
    public class ItemRepository : Repository<Item>
    {
        public ItemRepository(DataContext context) : base(context)
        {
        }

        public List<Item> GetAll()
        {
            return Get().ToList();
        }

        public bool CreatedBefore( int id, string name, string subName , int type)
        {
            name = name.ToLower();
            subName = subName.ToLower();
            return Get(o => o.Name.ToLower() == name && o.SubName == subName && o.Type == type && o.Id!=id).Any();
        }

        public void Create(Item item)
        {
            Add(item);
        }

        public Item GetByGuid(Guid id)
        {
            return Get(o => o.GUID == id).FirstOrDefault();
        }

        public void Edit(Item item)
        {
            Update(item);
        }

        public Item GetById(int elementId)
        {
            return Get(o => o.Id == elementId).FirstOrDefault();

        }

        public void Remove(int elementId)
        {
            Delete(elementId);
        }
    }
}

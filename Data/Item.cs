using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class Item:Entity
    {
        public Item()
        {
            ItemDetails = new HashSet<ItemDetails>();
        }
        public string Name { get; set; }
        public string SubName { get; set; }
        public int Type { get; set; }
        public decimal Price { get; set; }

        public ICollection<ItemDetails> ItemDetails { get; set; }
    }
}

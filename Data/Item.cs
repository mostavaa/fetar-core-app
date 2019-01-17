using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class Item:Entity
    {
        public string Name { get; set; }
        public string SubName { get; set; }
        public int Type { get; set; }
        public decimal Price { get; set; }

        public ItemDetails ItemDetails { get; set; }
    }
}

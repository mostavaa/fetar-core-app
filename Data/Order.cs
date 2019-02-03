using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class Order:Entity
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetails>();
            Ordered = false;
        }
        public string Name { get; set; }
        public ICollection<OrderDetails> OrderDetails { get; set; }
        public bool Ordered { get; set; }

    }
}

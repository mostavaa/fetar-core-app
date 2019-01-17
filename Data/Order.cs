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
        }
        public string Name { get; set; }
        public ICollection<OrderDetails> OrderDetails { get; set; }

    }
}

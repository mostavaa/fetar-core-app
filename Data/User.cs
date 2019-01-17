using System;
using System.Collections.Generic;

namespace Data
{
    public class User:Entity
    {
        public User()
        {
            OrderDetails = new HashSet<OrderDetails>();
        }
        public string Username { get; set; }
        public string Password { get; set; }

        public ICollection<OrderDetails> OrderDetails { get; set; }

    }
}

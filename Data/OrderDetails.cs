using System.Collections.Generic;

namespace Data
{
    public class OrderDetails:Entity
    {
        public OrderDetails()
        {
            Total = 0;
            Items = new HashSet<ItemDetails>();
        }
        public int? UserId { get; set; }
        public User User { get; set; }
        public int? OrderId { get; set; }
        public Order Order { get; set; }
        public ICollection<ItemDetails> Items { get; set; }
        public int Total { get; set; }
        public bool Payed { get; set; }
        public string Notes { get; set; }
    }
}

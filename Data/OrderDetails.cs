using System.Collections.Generic;

namespace Data
{
    public class OrderDetails:Entity
    {
        public OrderDetails()
        {
            ItemDetails = new HashSet<ItemDetails>();
            User = new User();
            Order = new Order();
        }
        public int? UserId { get; set; }
        public User User { get; set; }
        public int? OrderId { get; set; }
        public Order Order { get; set; }
        public ICollection<ItemDetails> ItemDetails { get; set; }
        public bool Payed { get; set; }
        public string Notes { get; set; }
    }
}

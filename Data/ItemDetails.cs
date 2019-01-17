namespace Data
{
    public class ItemDetails:Entity
    {
        public int? ItemId { get; set; }
        public Item Item { get; set; }

        public int? OrderDetailsId { get; set; }
        public OrderDetails OrderDetails { get; set; }
        public int Amount { get; set; }
    }
}

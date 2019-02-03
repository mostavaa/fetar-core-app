using System;
using System.ComponentModel.DataAnnotations;

namespace Services.ViewModels
{
    public class ItemDetailsVM
    {
        public ItemDetailsVM()
        {
            ItemVM = new ItemVM();
            OrderDetailsVM = new OrderDetailsVM();
        }
        public int? ItemId { get; set; }
        public ItemVM ItemVM { get; set; }

        public int? OrderDetailsId { get; set; }
        public OrderDetailsVM OrderDetailsVM { get; set; }
        public int Amount { get; set; }
        public int ItemDetailsId { get; set; }
        public Guid ItemDetailsGuid { get; set; }
        public string Notes { get; set; }

    }
}
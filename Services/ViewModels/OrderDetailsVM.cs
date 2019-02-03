using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Services.ViewModels
{
    public class OrderDetailsVM
    {
        public OrderDetailsVM()
        {
            OrderVM = new OrderVM();
            UserVM = new UserVM();
            ItemDetailsVM = new HashSet<ItemDetailsVM>();
        }
        public int? UserId { get; set; }
        public UserVM UserVM { get; set; }
        public int? OrderId { get; set; }
        public OrderVM OrderVM { get; set; }
        public ICollection<ItemDetailsVM> ItemDetailsVM { get; set; }
        public bool Payed { get; set; }
        public string Notes { get; set; }

        public int OrderDetailsId { get; set; }
        public Guid OrderDetailsGuid { get; set; }
    }
}

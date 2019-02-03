using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Services.ViewModels
{
    public class OrderVM
    {
        public OrderVM()
        {
            OrderDetailsVM = new HashSet<OrderDetailsVM>();
        }
        public string Name { get; set; }
        public string Date { get; set; }
        public Guid GUID { get; set; }

        public int OrderId { get; set; }
        public bool Ordered { get; set; }

        public ICollection<OrderDetailsVM> OrderDetailsVM { get; set; }

    }
}

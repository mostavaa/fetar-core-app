using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Services.ViewModels
{
    public class ItemVM
    {
        public ItemVM()
        {
            GUID = Guid.NewGuid();
            ItemDetailsVM = new HashSet<ItemDetailsVM>();
        }
        [Required]
        public string Name { get; set; }
        public string SubName { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string Price { get; set; }
        public Guid GUID { get; set; }
        public int ElementId { get; set; }

        public ICollection<ItemDetailsVM> ItemDetailsVM { get; set; }
        
    }
}

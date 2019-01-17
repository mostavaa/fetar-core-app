using System;
using System.ComponentModel.DataAnnotations;

namespace Services.ViewModels
{
    public class ItemVM
    {
        public ItemVM()
        {
            
            GUID = Guid.NewGuid();
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
    }
}

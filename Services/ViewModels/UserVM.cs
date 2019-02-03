using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Services.ViewModels
{
    public class UserVM
    {
        public UserVM()
        {
            OrderDetailsVM = new HashSet<OrderDetailsVM>();
        }
        [Required]
        [MinLength(2)]
        public string username { get; set; }
        [Required]
        [MinLength(6)]
        public string password { get; set; }

        public int UserId { get; set; }
        public int UserGuid { get; set; }

        public ICollection<OrderDetailsVM> OrderDetailsVM { get; set; }
    }
}

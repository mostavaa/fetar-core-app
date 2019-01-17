using System.ComponentModel.DataAnnotations;

namespace Services.ViewModels
{
    public class UserVM
    {
        [Required]
        [MinLength(2)]
        public string username { get; set; }
        [Required]
        [MinLength(6)]
        public string password { get; set; }
    }
}

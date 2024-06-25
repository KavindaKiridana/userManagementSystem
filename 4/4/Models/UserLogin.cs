using System.ComponentModel.DataAnnotations;

namespace _4.Models
{
    public class UserLogin
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

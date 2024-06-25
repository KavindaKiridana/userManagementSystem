using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace _4.Models
{
    public class UserDto
    {
        public UserDto()
        {
            if (string.IsNullOrEmpty(Password))
            {
                Password = Name + "@123";
            }
        }

        [Required]
        [DisplayName("User Name")]
        public string Name { get; set; } = "";

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _password = Name + "@123";
                }
                else
                {
                    _password = value;
                }
            }
        }

        [Required]
        public bool IsAdmin { get; set; } = false;
        public bool IsDeleted { get; set; } = false;
    }
}

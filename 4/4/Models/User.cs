using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using BCrypt.Net;

namespace _4.Models
{
    public class User
    {
        public User()
        {
            if (string.IsNullOrEmpty(Password))
            {
                Password = Name + "@123";
            }
        }

        [Key]
        public int ID { get; set; }

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
                    _password = BCrypt.Net.BCrypt.EnhancedHashPassword(Name + "@123");
                }
                else
                {
                    _password = BCrypt.Net.BCrypt.EnhancedHashPassword(value);
                }
            }
        }

        public bool IsAdmin { get; set; } = false;

        public bool IsDeleted { get; set; } = false;
    }
}

//i want to store this password after encripted by using this function "string hashedPassword=BCrypt.Net.BCrypt.EnhancedHashPassword(password);"
//give me the modified code 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagment.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Type { get; set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }

        public override bool Equals(object obj)
        {
            return obj is User user &&
                   Id == user.Id;
        }
    }
}

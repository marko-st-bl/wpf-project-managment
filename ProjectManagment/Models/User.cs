using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagment.Models
{
    public class User : IComparable
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

        public int CompareTo(object obj)
        {
            if(obj == null)
            {
                return 1;
            }
            User other = obj as User;
            if(other != null)
            {
                return this.FirstName.CompareTo(other.FirstName);
            }
            else
            {
                throw new ArgumentException("Other object is not user");
            }
        }
    }
}

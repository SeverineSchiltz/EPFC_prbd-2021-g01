using PRBD_Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prbd_2021_g01.Model {
    public abstract class User : EntityBase<EcoleContext>
    {
        [Key]
        public int Id { get; set; }

        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public User(string firstname, string lastname, string email, string password) {
            Firstname = firstname;
            Lastname = lastname;
            Email = email;
            Password = password;
        }

        public User() { }

        public static User GetByEmail(string email)
        {
            return Context.Users.SingleOrDefault(m => m.Email == email);
        }

        public override string ToString() {
            return $"{Firstname} {Lastname}";
        }

        //public string DiscriminatorValue
        //{
        //    get
        //    {
        //        return this.GetType().Name;
        //    }
        //}
    }
}

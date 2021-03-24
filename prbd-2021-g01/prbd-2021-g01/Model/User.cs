using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prbd_2021_g01.Model {
    public abstract class User {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public User(string email, string password) {
            Email = email;
            Password = password;
        }

        public User() { }
    }
}

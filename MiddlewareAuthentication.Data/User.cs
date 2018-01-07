using System;
using System.Collections.Generic;
using System.Text;

namespace MiddlewareAuthentication.Data
{
    public class User
    {
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string UserRole { get; set; }

        public User()
        {

        }

        public User(string Name,string password,string role)
        {
            UserName = Name;
            UserPassword = password;
            UserRole = role;
        }

    }
}

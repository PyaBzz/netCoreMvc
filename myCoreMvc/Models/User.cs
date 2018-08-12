using System;
using System.Collections.Generic;
using System.Linq;

namespace myCoreMvc.Models
{
    public class User
    {
        public string Name { get; }
        public DateTime DateOfBirth { get; }
        public string Role { get; }

        public User(string name, DateTime dob, string role)
        {
            Name = name;
            DateOfBirth = dob;
            Role = role;
        }
    }
}

using PooyasFramework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace myCoreMvc.Models
{
    public class User : Thing
    {
        public string Name { get; set; }
        public string Hash { get; set; }
        public string Salt { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Role { get; set; } //Task: Make Role a type it needs to be linked to Access Level somehow. Can we?

        public User()
        {
            //Salt = 
        }
    }
}

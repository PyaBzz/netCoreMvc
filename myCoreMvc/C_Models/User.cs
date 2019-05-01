using PyaFramework.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace myCoreMvc.Models
{
    public class User : Thing, IUser
    {
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Role { get; set; }
        public byte[] Salt { get; set; }
        public string Hash { get; set; }
    }
}

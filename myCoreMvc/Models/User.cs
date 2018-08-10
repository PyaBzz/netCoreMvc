using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myCoreMvc.Models
{
    public interface IUserService
    {
        Task<bool> ValidateCredentials(string userName, string passWord, out User user);
    }

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

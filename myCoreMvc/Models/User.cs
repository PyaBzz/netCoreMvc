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

        public User(string name)
        {
            Name = name;
        }
    }
}

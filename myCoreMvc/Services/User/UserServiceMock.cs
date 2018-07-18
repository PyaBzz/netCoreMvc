using myCoreMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myCoreMvc.Services
{
    public class UserServiceMock : IUserService
    {
        private IDictionary<string, (string PwHash, User User)> _users = new Dictionary<string, (string PwHash, User User)>();

        public UserServiceMock(IDictionary<string, string> users)
        {
            foreach (var user in users)
            {
                var hash = user.Value; //TODO: Hash the PW
                _users.Add(user.Key.ToLower(), (hash, new User(user.Key)));
            }
        }

        public Task<bool> ValidateCredentials(string userName, string passWord, out User user)
        {
            user = null;
            var key = userName.ToLower();
            if (_users.ContainsKey(key))
            {
                var existingHash = _users[key].PwHash;
                var hash = passWord; //TODO: Needs to change based on Hash implementation
                if (hash == existingHash)
                {
                    user = _users[key].User;
                    return Task.FromResult(true);
                }
            }
            return Task.FromResult(false);
        }
    }
}

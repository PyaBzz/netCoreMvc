using myCoreMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myCoreMvc.Services
{
    public class UserServiceMock : IUserService
    {
        private IDictionary<string, (string PwHash, User User)> Records;

        public UserServiceMock()
        {
            Records = new Dictionary<string, (string PwHash, User User)>();
            Records.Add("junior0", ("j00", new User("junior0", new DateTime(2018, 01, 01), "junior"))); //Task: Hash the PW
            Records.Add("senior0", ("s00", new User("senior00", new DateTime(2010, 01, 01), "senior"))); //Task: Hash the PW
            Records.Add("admin0", ("a00", new User("admin00", new DateTime(2000, 01, 01), "admin"))); //Task: Hash the PW
        }

        public Task<bool> ValidateCredentials(string userName, string passWord, out User user)
        {
            user = null;
            var key = userName.ToLower();
            if (Records.ContainsKey(key))
            {
                var existingHash = Records[key].PwHash;
                var hash = passWord;
                if (hash == existingHash)
                {
                    user = Records[key].User;
                    return Task.FromResult(true);
                }
            }
            return Task.FromResult(false);
        }
    }
}

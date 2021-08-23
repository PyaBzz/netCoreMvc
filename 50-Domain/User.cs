using myCoreMvc.Domain.Attributes;
using System;

namespace myCoreMvc.Domain
{
    public class User : Thing
    {
        [Persist]
        public string Name { get; set; }
        [Persist]
        public DateTime DateOfBirth { get; set; }
        [Persist]
        public string Role { get; set; }
        [Persist]
        public byte[] Salt { get; set; }
        [Persist]
        public string Hash { get; set; }
    }
}

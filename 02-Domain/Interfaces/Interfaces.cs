using PyaFramework.Core;
using System;

namespace myCoreMvc.Domain
{
    public interface IThing : IClonable
    {
        Guid Id { get; set; }
    }

    public interface IUser : IThing
    {
        string Name { get; set; }
        DateTime DateOfBirth { get; set; }
        string Role { get; set; } //Task: Make Role a type it needs to be linked to Access Level somehow. Can we?
        byte[] Salt { get; set; }
        string Hash { get; set; }
    }
}

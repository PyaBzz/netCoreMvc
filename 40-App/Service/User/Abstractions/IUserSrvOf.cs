using myCoreMvc.Domain;

namespace myCoreMvc.App.Services
{
    public interface IUserSrvOf
    {
        User User { get; }

        User Save();
        User SetPassword(string password);
        void Delete();
    }
}

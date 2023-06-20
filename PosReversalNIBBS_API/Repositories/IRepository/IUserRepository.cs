using PosReversalNIBBS_API.Models.Domain;

namespace PosReversalNIBBS_API.Repositories.IRepository
{
    public interface IUserRepository
    {
        Task<User> AuthenticateAsync(string username, string password);
    }
}

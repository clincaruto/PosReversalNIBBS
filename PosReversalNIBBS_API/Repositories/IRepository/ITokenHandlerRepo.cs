using PosReversalNIBBS_API.Models.Domain;

namespace PosReversalNIBBS_API.Repositories.IRepository
{
    public interface ITokenHandlerRepo
    {
        Task<string> CreateTokenAsync(User user);
    }
}

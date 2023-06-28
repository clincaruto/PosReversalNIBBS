using PosReversalNIBBS_API.Models.Domain;
using PosReversalNIBBS_API.Models.DTO;

namespace PosReversalNIBBS_API.Repositories.IRepository
{
    public interface ITokenHandlerRepo
    {
        Task<string> CreateTokenAsync(LoginRequest user);
    }
}

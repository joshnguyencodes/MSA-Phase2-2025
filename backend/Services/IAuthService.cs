using bulkbuy.api.Models;
using System.Threading.Tasks;

namespace bulkbuy.api.Services
{
    public interface IAuthService
    {
        Task<AuthResult> RegisterUser(User user);
        Task<AuthResult> LoginUser(LoginRequest loginRequest);
    }
}

using bulkbuy.api.Models;
using System.Threading.Tasks;

namespace bulkbuy.api.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByUsername(string username);
        Task AddUser(User user);
    }
}
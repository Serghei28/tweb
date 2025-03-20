using System.Collections.Generic;
using System.Threading.Tasks;
using YourProject.Domain.Models;

namespace YourProject.BusinessLogic.Interfaces
{
    public interface IUserService
    {
        Task<User> GetCurrentUser(); // Предположительно, должен получать текущего пользователя из контекста
        Task<User> GetUserById(int id);
        Task<User> UpdateUser(int id, User data);
        Task<Address> AddAddress(int userId, Address address);
        Task<Address> UpdateAddress(int addressId, Address data);
        Task<bool> DeleteAddress(int addressId);
        Task<UserWithAddress> GetUserWithAddresses(int userId);
    }
}

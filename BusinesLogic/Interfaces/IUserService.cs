using System.Collections.Generic;
using System.Threading.Tasks;
using YourProject.Domain.Models;

namespace YourProject.BusinessLogic.Interfaces
{
    public interface IUserService
    {
        Task<User> GetCurrentUser(); // (опционально)
        Task<User> GetUserById(int id);
        Task<User> CreateUser(User user);                  // ← новый метод
        Task<User> UpdateUser(int id, User data);
        Task<User> Authenticate(string email, string password); // ← новый метод
        Task<Address> AddAddress(int userId, Address address);
        Task<Address> UpdateAddress(int addressId, Address data);
        Task<bool> DeleteAddress(int addressId);
        Task<UserWithAddress> GetUserWithAddresses(int userId);
        Task<User> GetUserByCredentials(string email, string password);
        Task<List<User>> GetAllUsers();
        Task<bool> DeleteUser(int id);
    }
}

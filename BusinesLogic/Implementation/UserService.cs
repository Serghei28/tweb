using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YourProject.Domain.Models;
using YourProject.BusinessLogic.Interfaces;

namespace YourProject.BusinessLogic.Implementation
{
    public class UserService : IUserService
    {
        // Временное хранилище пользователей и адресов для демонстрации
        private readonly List<User> _users = new List<User>();
        private readonly List<Address> _addresses = new List<Address>();

        public UserService()
        {
            // Инициализация демонстрационными данными
            var user = new User(1, "john.doe@example.com", "John", "Doe", "123456789");
            _users.Add(user);
            _addresses.Add(new Address(1, user.Id, "USA", "New York", "5th Avenue", "10001", true));
        }

        public async Task<User> GetCurrentUser()
        {
            // Возвращаем первого пользователя в качестве текущего (для демонстрации)
            return await Task.FromResult(_users.First());
        }

        public async Task<User> GetUserById(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user == null)
                throw new Exception("User not found");
            return await Task.FromResult(user);
        }

        public async Task<User> UpdateUser(int id, User data)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user == null)
                throw new Exception("User not found");

            user.Email = data.Email;
            user.FirstName = data.FirstName;
            user.LastName = data.LastName;
            user.Phone = data.Phone;
            user.UpdatedAt = DateTime.UtcNow;

            return await Task.FromResult(user);
        }

        public async Task<Address> AddAddress(int userId, Address address)
        {
            // В реальном проекте необходимо проверить существование пользователя
            address.Id = _addresses.Count + 1;
            address.UserId = userId;
            _addresses.Add(address);
            return await Task.FromResult(address);
        }

        public async Task<Address> UpdateAddress(int addressId, Address data)
        {
            var address = _addresses.FirstOrDefault(a => a.Id == addressId);
            if (address == null)
                throw new Exception("Address not found");

            address.Country = data.Country;
            address.City = data.City;
            address.Street = data.Street;
            address.ZipCode = data.ZipCode;
            address.IsDefault = data.IsDefault;

            return await Task.FromResult(address);
        }

        public async Task<bool> DeleteAddress(int addressId)
        {
            var address = _addresses.FirstOrDefault(a => a.Id == addressId);
            if (address == null)
                return await Task.FromResult(false);
            _addresses.Remove(address);
            return await Task.FromResult(true);
        }

        public async Task<UserWithAddress> GetUserWithAddresses(int userId)
        {
            var user = _users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
                throw new Exception("User not found");
            var userAddresses = _addresses.Where(a => a.UserId == userId).ToList();
            return await Task.FromResult(new UserWithAddress(user, userAddresses));
        }
    }
}

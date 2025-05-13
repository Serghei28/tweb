using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YourProject.Domain.Models; // Укажи правильное пространство имён моделей
using YourProject.BusinessLogic.Interfaces;
using tweb.DAL.Data;      // Подключи AppDbContext

namespace tweb.BusinessLogic.Implementation
{
    public class UserService : IUserService
    {
        public async Task<User> GetCurrentUser()
        {
            using (var db = new AppDbContext())
            {
                return await Task.FromResult(db.Users.FirstOrDefault());
            }
        }

        public async Task<User> GetUserById(int id)
        {
            using (var db = new AppDbContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Id == id);
                if (user == null)
                    throw new Exception("User not found");
                return await Task.FromResult(user);
            }
        }

        public async Task<User> UpdateUser(int id, User data)
        {
            using (var db = new AppDbContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Id == id);
                if (user == null)
                    throw new Exception("User not found");

                user.Email = data.Email;
                user.FirstName = data.FirstName;
                user.LastName = data.LastName;
                user.Phone = data.Phone;
                user.UpdatedAt = DateTime.UtcNow;

                db.SaveChanges();

                return await Task.FromResult(user);
            }
        }

        public async Task<Address> AddAddress(int userId, Address address)
        {
            using (var db = new AppDbContext())
            {
                // Проверка пользователя
                var user = db.Users.FirstOrDefault(u => u.Id == userId);
                if (user == null)
                    throw new Exception("User not found");

                address.UserId = userId;
                db.Addresses.Add(address);
                db.SaveChanges();

                return await Task.FromResult(address);
            }
        }

        public async Task<Address> UpdateAddress(int addressId, Address data)
        {
            using (var db = new AppDbContext())
            {
                var address = db.Addresses.FirstOrDefault(a => a.Id == addressId);
                if (address == null)
                    throw new Exception("Address not found");

                address.Country = data.Country;
                address.City = data.City;
                address.Street = data.Street;
                address.ZipCode = data.ZipCode;
                address.IsDefault = data.IsDefault;

                db.SaveChanges();

                return await Task.FromResult(address);
            }
        }

        public async Task<bool> DeleteAddress(int addressId)
        {
            using (var db = new AppDbContext())
            {
                var address = db.Addresses.FirstOrDefault(a => a.Id == addressId);
                if (address == null)
                    return await Task.FromResult(false);

                db.Addresses.Remove(address);
                db.SaveChanges();

                return await Task.FromResult(true);
            }
        }

        public async Task<UserWithAddress> GetUserWithAddresses(int userId)
        {
            using (var db = new AppDbContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Id == userId);
                if (user == null)
                    throw new Exception("User not found");

                var addresses = db.Addresses.Where(a => a.UserId == userId).ToList();

                return await Task.FromResult(new UserWithAddress(user, addresses));
            }
        }
    }
}

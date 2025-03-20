using System.Collections.Generic;
using System.Linq;
using YourProject.Domain.Models;

namespace YourProject.BusinessLogic.API
{
    public class UserAPI : BaseAPI
    {
        private readonly List<User> _users;
        private readonly List<Address> _addresses;

        public UserAPI()
        {
            _users = new List<User>
            {
                new User(1, "alice@example.com", "Alice", "Smith", "123456789"),
                new User(2, "bob@example.com", "Bob", "Johnson", "987654321")
            };

            _addresses = new List<Address>
            {
                new Address(1, 1, "USA", "New York", "123 Main St", "10001", true),
                new Address(2, 1, "USA", "Los Angeles", "456 Elm St", "90001", false),
                new Address(3, 2, "USA", "Chicago", "789 Oak St", "60601", true)
            };
        }

        public User GetUser(int id)
        {
            return _users.FirstOrDefault(u => u.Id == id);
        }

        public List<Address> GetUserAddresses(int userId)
        {
            return _addresses.Where(a => a.UserId == userId).ToList();
        }
    }
}

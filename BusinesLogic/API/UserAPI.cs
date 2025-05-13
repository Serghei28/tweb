using System.Collections.Generic;
using System.Linq;
using YourProject.Domain.Models;       // Укажи своё реальное пространство имён моделей
using tweb.DAL.Data;           // Подключи AppDbContext
using YourProject.BusinessLogic.API;  // если BaseAPI в том же проекте

namespace tweb.BusinessLogic.API
{
    public class UserAPI : BaseAPI
    {
        public User GetUser(int id)
        {
            using (var db = new AppDbContext())
            {
                return db.Users.FirstOrDefault(u => u.Id == id);
            }
        }

        public List<Address> GetUserAddresses(int userId)
        {
            using (var db = new AppDbContext())
            {
                return db.Addresses.Where(a => a.UserId == userId).ToList();
            }
        }
    }
}

using System.Collections.Generic;
using YourProject.Domain.Models;

namespace popitka.ViewModels
{
    public class AdminPanelViewModel
    {
        public List<User> Users { get; set; }
        public List<Order> Orders { get; set; }
    }
}

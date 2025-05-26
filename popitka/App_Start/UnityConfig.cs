using System.Web.Mvc;
using tweb.BusinessLogic.Implementation;
using Unity;
using Unity.Mvc5;
using YourProject.BusinessLogic.Implementation;
using YourProject.BusinessLogic.Interfaces;
using YourProject.BusinessLogic.Services;

namespace popitka
{
    public static class UnityConfig
    {
        private static IUnityContainer container;

        public static IUnityContainer Container => container;

        public static void RegisterComponents()
        {
            container = new UnityContainer();

            container.RegisterType<IUserService, UserService>();
            container.RegisterType<IProductService, ProductService>();
            container.RegisterType<IOrderService, OrderService>();
            container.RegisterType<IPaymentService, PaymentService>();


            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}

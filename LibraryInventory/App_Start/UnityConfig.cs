using LibraryInventory.Data;
using LibraryInventory.Models;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace LibraryInventory.App_Start
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            container.RegisterType<IRepository<Book>, Repository<Book>>();
            container.RegisterType<IRepository<Category>, Repository<Category>>();
            container.RegisterType<ICategoryRepository, CategoryRepository>(); // Register the new repository

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}
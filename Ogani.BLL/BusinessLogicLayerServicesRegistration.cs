using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ogani.BLL.Services;
using Ogani.BLL.Services.Contracts;
using Ogani.BLL.UI.Services;
using Ogani.BLL.UI.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ogani.BLL
{
    public static class BusinessLogicLayerServicesRegistration
    {
        public static IServiceCollection AddBllServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped(typeof(ICrudService<,,,>),typeof(CrudManager<,,,>));
            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<IProductService, ProductManager>();
            services.AddScoped<IHomeService, HomeManager>();
            services.AddScoped<CloudinaryManager>();
            services.AddScoped<IAccountService, AccountManager>();


            return services;
        }
    }
}

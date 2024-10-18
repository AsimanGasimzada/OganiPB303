using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Ogani.DAL.DataContext;
using Ogani.DAL.DataContext.Entities;
using Ogani.DAL.Repositories;
using Ogani.DAL.Repositories.Contracts;

namespace Ogani.DAL
{
    public static class DataAccessLayerServiceRegistration
    {
        
        public static IServiceCollection AddDalServices(this IServiceCollection services, IConfiguration configuration )
        {
            
            services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("default"),
            builder =>
            {
                builder.MigrationsAssembly("Ogani.DAL");
            }));

            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 3;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;

                options.User.RequireUniqueEmail = true;

                options.Lockout.MaxFailedAccessAttempts = 7;
                options.Lockout.DefaultLockoutTimeSpan=TimeSpan.FromMinutes(5);
                
            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();


            services.AddScoped(typeof(IRepository<>), typeof(EfCoreRepository<>));
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
            services.AddScoped<IProductImageRepository, ProductImageRepository>();
            services.AddScoped<DataInitalizer>();

            return services;
        }
    }
}

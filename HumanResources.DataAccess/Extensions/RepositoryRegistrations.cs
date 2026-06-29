using HumanResources.DataAccess.Context;
using HumanResources.DataAccess.IdentityValidations;
using HumanResources.DataAccess.Interceptors;
using HumanResources.DataAccess.UOW;
using HumanResources.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace HumanResources.DataAccess.Extensions
{
    public static class RepositoryRegistrations
    {

        public static IServiceCollection AddRepositoriesExt(this IServiceCollection services,IConfiguration configuration)
        {
            //DATABASE AYARLARMAIR
            services.AddDbContext<AppDbContext>(options =>
            {

                options.UseNpgsql(configuration.GetConnectionString("PostgreSQL")); //secret.json 'da.
                options.AddInterceptors(new AuditDbContextInterceptor());

            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();


            //Scrutor ile Registration

            services.Scan(options => options.FromAssemblies(Assembly.GetExecutingAssembly())
                    .AddClasses(x => x.Where(t => t.Name.EndsWith("Repository")))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime()

            );


            //Identity
            services.AddIdentity<AppUser, AppRole>(options =>
            {

                options.User.RequireUniqueEmail = true;//ayný emaildn birden fazla olmayacak


            }).AddEntityFrameworkStores<AppDbContext>()
              .AddErrorDescriber<CustomErrorDescriber>();


            return services;

        }



    }
}

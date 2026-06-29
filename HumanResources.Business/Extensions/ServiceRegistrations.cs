using FluentValidation;
using HumanResources.Business.MapsterConfigs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace HumanResources.Business.Extensions
{
    public static class ServiceRegistrations
    {
        public static IServiceCollection AddServiceExt(this IServiceCollection services,IConfiguration configuration)
        {
            //SCRUTOR ÝLE AUTO REGÝSTRATÝON

            services.Scan(options => options.FromAssemblies(Assembly.GetExecutingAssembly()) //kendi assembly içinde 
                     .AddClasses(x => x.Where(t => t.Name.EndsWith("Service")))
                     .AsImplementedInterfaces()
                     .WithScopedLifetime()

            );

            // services.AddValidatorsFromAssembly(typeof(CreateAppointmentValidator).Assembly);
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            DepartmentConfig.Configure(); 

            return services;
        }





    }
}

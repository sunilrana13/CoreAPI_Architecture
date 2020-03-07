using Microsoft.Extensions.DependencyInjection;
using Sample.Repository;
using Sample.RepositoryContract;
using Sample.Service;
using Sample.ServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Sample.Web.API.Dependency
{
    public static class DependencyResolver
    {
        public static void AddServiceResolver(this IServiceCollection services)
        {
            services.AddTransient<IEmployeeService, EmployeeService>();
           services.AddRepositoryResolver();
        }

        public static void AddRepositoryResolver(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
           
        }
    }
}

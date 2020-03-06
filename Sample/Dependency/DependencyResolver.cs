using Microsoft.Extensions.DependencyInjection;
using Sample.RepositoryContract;
using Sample.Service;
using Sample.Service.Dependency;
using Sample.ServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Sample.Web.API.Dependency
{
    public static class DependencyResolver
    {
        public static void ServiceResolver(this IServiceCollection services)
        {
            services.AddTransient<IEmployeeService, EmployeeService>();
            services.RepositoryResolver();
        }
    }
}

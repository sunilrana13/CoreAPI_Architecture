using Microsoft.Extensions.DependencyInjection;
using Sample.Repository;
using Sample.RepositoryContract;
using System;
using System.Collections.Generic;
using System.Text;
//using Microsoft.Extensions.DependencyInjection;

namespace Sample.Service.Dependency
{
    public static class DependencyResolver
    {
        public static void RepositoryResolver(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        }
    }
}

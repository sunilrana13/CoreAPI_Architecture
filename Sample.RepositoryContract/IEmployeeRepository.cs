using Sample.DataContract;
using System;
using System.Collections.Generic;

namespace Sample.RepositoryContract
{
    public interface IEmployeeRepository
    {
        List<Employee> GetEmployee();
    }
}

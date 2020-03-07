using System;
using System.Collections.Generic;
using Sample.DataContract;

namespace Sample.ServiceContract
{
    public interface IEmployeeService
    {
        List<Employee> GetEmployee();
    }
}

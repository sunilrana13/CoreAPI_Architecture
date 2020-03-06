using System;
using System.Collections.Generic;
using Sample.Model;

namespace Sample.ServiceContract
{
    public interface IEmployeeService
    {
        List<EmployeeDTO> GetEmployee();
    }
}

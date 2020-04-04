using Sample.Dtos;
using System;
using System.Collections.Generic;


namespace Sample.ServiceContract
{
    public interface IEmployeeService
    {
        List<EmployeeDTO> GetEmployee();
    }
}

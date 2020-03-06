using Sample.Model;
using System;
using System.Collections.Generic;

namespace Sample.RepositoryContract
{
    public interface IEmployeeRepository
    {
        List<EmployeeDTO> GetEmployee();
    }
}

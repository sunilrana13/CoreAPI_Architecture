using Sample.DataContract;
using Sample.RepositoryContract;
using Sample.ServiceContract;
using System;
using System.Collections.Generic;

namespace Sample.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public List<Employee> GetEmployee()
        {
           return _employeeRepository.GetEmployee();
        }
    }
}

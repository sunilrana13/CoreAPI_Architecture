using Sample.DataContract;

using Sample.RepositoryContract;
using System;
using System.Collections.Generic;


namespace Sample.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public EmployeeRepository()
        {

        }
        public List<Employee> GetEmployee()
        {
            throw new NotImplementedException();
        }
    }
}

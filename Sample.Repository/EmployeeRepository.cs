using Sample.Model;
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
        public List<EmployeeDTO> GetEmployee()
        {
            throw new NotImplementedException();
        }
    }
}

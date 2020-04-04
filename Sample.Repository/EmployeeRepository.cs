using Sample.DataContract;
using Sample.Repository.Data;
using Sample.RepositoryContract;
using System;
using System.Collections.Generic;


namespace Sample.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DBObjectContext _context;
        public EmployeeRepository(DBObjectContext context)
        {
            _context = context;
        }
        public List<Employee> GetEmployee()
        {
            return new List<Employee> { new Employee { EmployeeId = 1, EmployeeName = "Sunil Rana", EmailAddress = "sunil@gmail.com", EmployeeCode = "1001", Location = "Chandigarh" },
            new Employee { EmployeeId = 2, EmployeeName = "Anil Rana", EmailAddress = "anil@gmail.com", EmployeeCode = "1002", Location = "Chandigarh" } };
        }
    }
}

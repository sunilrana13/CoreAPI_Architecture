using Microsoft.Extensions.Options;
using Sample.RepositoryContract;
using System;

namespace Sample.Repository.Data
{
    public sealed class UnityOfWork : IUnitOfWork
    {

        private readonly DBObjectContext _context;
        private EmployeeRepository _employeeRepository;
        public UnityOfWork(DBObjectContext context)
        {
            _context = context;            
        }
        public IEmployeeRepository Employees => _employeeRepository = _employeeRepository ?? new EmployeeRepository(_context);


        public int Commit()
        {
            return  _context.SaveChanges();
        }
        public void Dispose()
        {
            if(_context != null)
                _context.Dispose();
           
        }
    }
}

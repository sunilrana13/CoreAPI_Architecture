using Sample.DataContract;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sample.RepositoryContract
{
    public interface IUnitOfWork : IDisposable
    {
        IEmployeeRepository Employees { get; }
        int Commit();
    }
}

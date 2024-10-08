using Infrastructure.Data.Postgres.Entities;
using Infrastructure.Repositories.Interface;

namespace Infrastructure.Data.Postgres
{
    public interface IUnitOfWork : IDisposable
    {

        IDepartmentRepository Departments { get; }
        IEmployeeRepository Employees { get; }
        IEmployeeRoleRepository EmployeeRoles { get; }
        IRoleRepository Roles { get; }

        Task<int> CommitAsync(); 
    }
}


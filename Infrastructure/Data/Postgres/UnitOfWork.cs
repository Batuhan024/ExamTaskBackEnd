using Core.Utilities;
using Infrastructure.Data.Postgres.Entities.Base.Interface;
using Infrastructure.Data.Postgres.EntityFramework;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Postgres;

public class UnitOfWork : IUnitOfWork
{
    private readonly PostgresContext _postgresContext;
    public UnitOfWork(PostgresContext postgresContext)
    {
        _postgresContext = postgresContext;
    }

    
    private DepartmentRepository? _departmentRepository;
    private EmployeeRepository? _employeeRepository;
    private EmployeeRoleRepository? _employeeRoleRepository;
    private RoleRepository? _roleRepository;



    public IDepartmentRepository Departments => _departmentRepository ??= new DepartmentRepository(_postgresContext);
    public IEmployeeRepository Employees => _employeeRepository ??= new EmployeeRepository(_postgresContext);
    public IEmployeeRoleRepository EmployeeRoles => _employeeRoleRepository ??= new EmployeeRoleRepository(_postgresContext);
    public IRoleRepository Roles => _roleRepository ??= new RoleRepository(_postgresContext);



    public async Task<int> CommitAsync()
    {
        var updatedEntities = _postgresContext.ChangeTracker.Entries<IEntity>()
            .Where(e => e.State == EntityState.Modified)
            .Select(e => e.Entity);

        foreach (var updatedEntity in updatedEntities)
        {
            updatedEntity.UpdatedAt = DateTime.UtcNow.ToTimeZone();
        }

        var result = await _postgresContext.SaveChangesAsync();

        return result;
    }

    public void Dispose()
    {
        _postgresContext.Dispose();
    }
}

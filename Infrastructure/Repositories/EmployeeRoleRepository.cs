using Infrastructure.Data.Postgres.EntityFramework;
using Infrastructure.Data.Postgres.Repositories.Base;
using Infrastructure.Data.Postgres.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Data.Postgres.Entities;
using Infrastructure.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class EmployeeRoleRepository : Repository<EmployeeRole, int>, IEmployeeRoleRepository
    {
        private readonly PostgresContext _context;
        public EmployeeRoleRepository(PostgresContext postgresContext) : base(postgresContext)
        {
            _context = postgresContext;
        }

        public IQueryable<EmployeeRole> GetEmployeeRoles()
        {
            return _context.EmployeeRoles.AsQueryable();
        }
    }
}

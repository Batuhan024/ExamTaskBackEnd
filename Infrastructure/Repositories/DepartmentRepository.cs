using Infrastructure.Data.Postgres.EntityFramework;
using Infrastructure.Data.Postgres.Repositories.Base;
using Infrastructure.Data.Postgres.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Repositories.Interface;
using Infrastructure.Data.Postgres.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class DepartmentRepository : Repository<Department, int>, IDepartmentRepository
    {
        private readonly PostgresContext _context;
        public DepartmentRepository(PostgresContext postgresContext) : base(postgresContext)
        {
            _context = postgresContext;
        }
        public async Task<bool> AnyAsync(Expression<Func<Department, bool>> predicate)
        {
            return await _context.Departments.AnyAsync(predicate);
        }
    }
}

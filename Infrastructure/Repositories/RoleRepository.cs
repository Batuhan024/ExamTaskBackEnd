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
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class RoleRepository : Repository<Role, int>, IRoleRepository
    {
        private readonly PostgresContext _context;
        public RoleRepository(PostgresContext postgresContext) : base(postgresContext)
        {
            _context = postgresContext;
        }
        public async Task<bool> AnyAsync(Expression<Func<Role, bool>> predicate)
        {
            return await _context.Roles.AnyAsync(predicate);
        }
    }
}

using Infrastructure.Data.Postgres.Entities;
using Infrastructure.Data.Postgres.Repositories.Base.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Interface
{
    public interface IRoleRepository : IRepository<Role, int>
    {
        Task<bool> AnyAsync(Expression<Func<Role, bool>> predicate);
    }
}

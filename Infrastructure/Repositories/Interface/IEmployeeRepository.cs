using Infrastructure.Data.Postgres.Entities;
using Infrastructure.Data.Postgres.Repositories.Base.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Interface
{
    public interface IEmployeeRepository : IRepository<Employee, int>
    {
        Employee GetByUsername(string email);

        Task<bool> ExistsAsync(string email);

        IQueryable<Employee> GetAllEmployees();
    }
}

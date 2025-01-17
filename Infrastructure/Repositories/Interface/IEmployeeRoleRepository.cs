﻿using Infrastructure.Data.Postgres.Entities;
using Infrastructure.Data.Postgres.Repositories.Base.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Interface
{
    public interface IEmployeeRoleRepository : IRepository<EmployeeRole, int>
    {
        IQueryable<EmployeeRole> GetEmployeeRoles();
    }
}

using Infrastructure.Data.Postgres.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Postgres.Entities
{
    public class EmployeeRole : Entity<int>
    {
        public int EmployeeID { get; set; }
        public int RoleID { get; set; }
    }
}

using Infrastructure.Data.Postgres.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Postgres.Entities
{
    public class Role : Entity<int>
    {
        public string Name { get; set; } = default!;
        public bool IsAdmin { get; set; } = default!;
    }
}

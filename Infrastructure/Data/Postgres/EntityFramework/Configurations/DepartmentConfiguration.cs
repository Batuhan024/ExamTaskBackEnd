using Core.Utilities;
using Infrastructure.Data.Postgres.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Postgres.EntityFramework.Configurations
{
    public class DepartmentConfiguration : BaseConfiguration<Department, int>
    {
        public override void Configure(EntityTypeBuilder<Department> builder)
        {
            base.Configure(builder);
            var data = new Department[]
            {
            new Department{ Id = 1,Name="Information Tech.",CreatedAt = DateTime.UtcNow.ToTimeZone(),IsDeleted=false},
            new Department{ Id = 2,Name="Human Researcher",CreatedAt = DateTime.UtcNow.ToTimeZone(),IsDeleted=false},
            new Department{ Id = 3,Name="Accounting",CreatedAt = DateTime.UtcNow.ToTimeZone(),IsDeleted=false},
            new Department{ Id = 4,Name="Sales",CreatedAt = DateTime.UtcNow.ToTimeZone(),IsDeleted=false},
            new Department{ Id = 5,Name="Purchasing",CreatedAt = DateTime.UtcNow.ToTimeZone(),IsDeleted=false},
            };

            builder.HasData(data);
        }
    }
}

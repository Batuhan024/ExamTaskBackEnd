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
    public class RoleConfiguration : BaseConfiguration<Role, int>
    {
        public override void Configure(EntityTypeBuilder<Role> builder)
        {
            base.Configure(builder);
            var data = new Role[]
            {
            new Role{ Id = 1,Name="Manager",IsAdmin=true,CreatedAt = DateTime.UtcNow.ToTimeZone(),IsDeleted=false},
            new Role{ Id = 2,Name="Director",IsAdmin=true,CreatedAt = DateTime.UtcNow.ToTimeZone(),IsDeleted=false},
            new Role{ Id = 3,Name="Software Developer",IsAdmin=false,CreatedAt = DateTime.UtcNow.ToTimeZone(),IsDeleted=false},
            new Role{ Id = 4,Name="Sales Representative",IsAdmin=false,CreatedAt = DateTime.UtcNow.ToTimeZone(),IsDeleted=false},
            new Role{ Id = 5,Name="Purchasing Officer",IsAdmin=false,CreatedAt = DateTime.UtcNow.ToTimeZone(),IsDeleted=false},
            };

            builder.HasData(data);
        }
    }
}

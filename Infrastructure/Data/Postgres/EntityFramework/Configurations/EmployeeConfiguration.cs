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
    public class EmployeeConfiguration : BaseConfiguration<Employee, int>
    {
        public override void Configure(EntityTypeBuilder<Employee> builder)
        {
            base.Configure(builder);
            var data = new Employee[]
            {
            new Employee{ Id = 1,FullName="Batuhan Yavuz",Email="batuhan@",Phone="0544",Password="123",
            DepartmentId=1,CreatedAt = DateTime.UtcNow.ToTimeZone(),IsDeleted=false},
            new Employee{ Id = 2,FullName="Utku Yavuz",Email="utku@",Phone="0545",Password="123",
            DepartmentId=1,CreatedAt = DateTime.UtcNow.ToTimeZone(),IsDeleted=false},
            new Employee{ Id = 3,FullName="Alperen Karakuş",Email="alperen@",Phone="0546",Password="123",
            DepartmentId=2,CreatedAt = DateTime.UtcNow.ToTimeZone(),IsDeleted=false},
            new Employee{ Id = 4,FullName="Berke Taşkur",Email="berke@",Phone="0547",Password="123",
            DepartmentId=3,CreatedAt = DateTime.UtcNow.ToTimeZone(),IsDeleted=false},
            new Employee{ Id = 5,FullName="Berkay Öztürk",Email="berkay@",Phone="0548",Password="123",
            DepartmentId=4,CreatedAt = DateTime.UtcNow.ToTimeZone(),IsDeleted=false},
            };

            builder.HasData(data);
        }
    }
}

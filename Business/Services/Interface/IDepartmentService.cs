using Business.Models.Response;
using Business.Services.Base.Interface;
using Core.Results;
using Infrastructure.Data.Postgres.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Interface
{
    public interface IDepartmentService : IBaseService<Department, int, DepartmentResponseDTO>
    {

        Task<Result> CreateDepartmentAsync(Department department);
    }
}

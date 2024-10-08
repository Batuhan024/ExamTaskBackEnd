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
    public interface IEmployeeService : IBaseService<Employee, int, EmployeeResponseDTO>
    {
        // Yeni bir çalışan ekleme metodu email boş mu ve email var mı kontrol
        Task<Result> CreateEmployeeAsync(Employee employee);

        // Çalışanı güncelleme metodu email boş mu ve email var mı kontrol
        Task<Result> UpdateEmployeeAsync(Employee employee);

        Task<List<EmployeeResponseDTO>> GetEmployeesByRoleNameAsync(string roleName);
        Task<List<EmployeeResponseDTO>> GetEmployeesByDepartmentNameAsync(string departmentName);

        Task<Result> AssignRolesToEmployeeAsync(int adminUserId, int employeeId, List<int> roleIds);
    }
}

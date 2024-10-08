using Business.Models.Request.Create;
using Business.Models.Request.Update;
using Business.Models.Response;
using Business.Services.Interface;
using Infrastructure.Data.Postgres.Entities;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.Base;

namespace ExamTask.Controllers
{
    public class EmployeeController : BaseCRUDController<Employee, int, EmployeeCreateDTO, EmployeeUpdateDTO, EmployeeResponseDTO>
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService service) : base(service)
        {
            _employeeService = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployeeContolEmail([FromBody] EmployeeCreateDTO employeeCreateDto)
        {
            var employee = new Employee
            {
                FullName = employeeCreateDto.FullName,
                Email = employeeCreateDto.Email,
                Phone = employeeCreateDto.Phone,
                DepartmentId = employeeCreateDto.DepartmentId,
                Password = employeeCreateDto.Password,
    };

            var result = await _employeeService.CreateEmployeeAsync(employee);

            if (!result.IsSuccess)
            {
                return BadRequest(new { Errors = result.Message });
            }

            return Ok(new
            {
                Message = "Çalışan başarıyla oluşturuldu.",
                Data = new
                {
                    employee.Id,
                    employee.FullName,
                    employee.Email,
                    employee.DepartmentId
                }
            });
        }



        [HttpGet]
        public async Task<IActionResult> GetEmployeesByRoleName(string roleName)
        {
            try
            {
                var employees = await _employeeService.GetEmployeesByRoleNameAsync(roleName);

                if (employees == null || employees.Count == 0)
                {
                    return NotFound(new { Message = $"'{roleName}' rolüne sahip hiçbir çalışan bulunamadı." });
                }

                return Ok(new
                {
                    Message = $"'{roleName}' rolüne sahip çalışanlar başarıyla getirildi.",
                    Data = employees
                });
            }
            catch (ArgumentException ex)
            {
                // Eğer böyle bir rol yoksa 400 BadRequest ile hata mesajı döndür
                return BadRequest(new { Message = ex.Message });
            }
        }



        [HttpGet]
        public async Task<IActionResult> GetEmployeesByDepartmentName(string departmentName)
        {
            try
            {
                var employees = await _employeeService.GetEmployeesByDepartmentNameAsync(departmentName);

                if (employees == null || employees.Count == 0)
                {
                    return NotFound(new { Message = $"'{departmentName}' departmanına sahip hiçbir çalışan bulunamadı." });
                }

                return Ok(new
                {
                    Message = $"'{departmentName}' departmanına sahip çalışanlar başarıyla getirildi.",
                    Data = employees
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }




        [HttpPost]
        public async Task<IActionResult> AssignRolesToEmployee([FromBody] RoleAssigmentCreateDTO request)
        {
            var result = await _employeeService.AssignRolesToEmployeeAsync(request.AdminUserId, request.EmployeeId, request.RoleIds);

            if (!result.IsSuccess)
            {
                return BadRequest(new { Errors = result.Message });
            }

            return Ok(new { Message = result.Message });
        }

    }
}

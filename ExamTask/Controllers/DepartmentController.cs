using Business.Models.Request.Create;
using Business.Models.Request.Update;
using Business.Models.Response;
using Business.Services.Interface;
using Infrastructure.Data.Postgres.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Web.Controllers.Base;

namespace ExamTask.Controllers
{
    public class DepartmentController : BaseCRUDController<Department, int, DepartmentCreateDTO, DepartmentUpdateDTO, DepartmentResponseDTO>
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService service) : base(service)
        {
            _departmentService = service;
        }

        // Yeni bir departman oluşturma işlemi isim kontrollü
        [HttpPost]
        public async Task<IActionResult> CreateDepartmentAsyncControlName([FromBody] DepartmentCreateDTO departmentCreateDto)
        {

            var department = new Department
            {
                Name = departmentCreateDto.Name
            };
            var result = await _departmentService.CreateDepartmentAsync(department);

            if (!result.IsSuccess)
            {
                return BadRequest(new
                {
                    Message = "Departman eklenemedi.",
                    Errors = result.Message
                });
            }

            return Ok(new
            {
                Message = "Departman başarıyla oluşturuldu.",
                Data = new
                {
                    department.Id,
                    department.Name
                }
            });
        }
    }
}

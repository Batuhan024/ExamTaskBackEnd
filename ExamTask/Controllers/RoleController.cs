using Business.Models.Request.Create;
using Business.Models.Request.Update;
using Business.Models.Response;
using Business.Services.Interface;
using Infrastructure.Data.Postgres.Entities;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.Base;
using System.Threading.Tasks;

namespace ExamTask.Controllers
{

    public class RoleController : BaseCRUDController<Role, int, RoleCreateDTO, RoleUpdateDTO, RoleResponseDTO>
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService service) : base(service)
        {
            _roleService = service;
        }


        [HttpPost("CreateRole")]
        public async Task<IActionResult> CreateRoleAsyncNameControl([FromBody] RoleCreateDTO roleCreateDto)
        {
            var role = new Role
            {
                Name = roleCreateDto.Name,
                IsAdmin = roleCreateDto.IsAdmin
            };

            var result = await _roleService.CreateRoleAsync(role);

            if (!result.IsSuccess)
            {
                return BadRequest(new { Errors = result.Message });
            }

            return Ok(new
            {
                Message = "Rol başarıyla oluşturuldu.",
                Data = new
                {
                    role.Id,
                    role.Name,
                    role.IsAdmin
                }
            });
        }
    }
}

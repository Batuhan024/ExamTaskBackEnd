using AutoMapper;
using Business.Models.Request.Create;
using Business.Models.Request.Functional;
using Business.Models.Request.Update;
using Business.Models.Response;
using Infrastructure.Data.Postgres.Entities;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Security;
using System.Text.RegularExpressions;
using File = Infrastructure.Data.Postgres.Entities.File;

namespace Business.Utilities.Mapping
{
    public class Profiles : Profile
    {
        public Profiles()
        {
            // CreateMap for Admin Entity and DTOs
            CreateMap<Employee, EmployeeResponseDTO>(); // Admin -> AdminResponseDTO mapping
            CreateMap<EmployeeCreateDTO, Employee>();   // AdminCreateDTO -> Admin mapping
            CreateMap<EmployeeUpdateDTO, Employee>();   // AdminUpdateDTO -> Admin mapping

            // CreateMap for other entities and their respective DTOs
            CreateMap<Department, DepartmentResponseDTO>();
            CreateMap<DepartmentCreateDTO, Department>();
            CreateMap<DepartmentUpdateDTO, Department>();

            CreateMap<EmployeeRole, EmployeeRoleResponseDTO>();
            CreateMap<EmployeeRoleCreateDTO, EmployeeRole>();
            CreateMap<EmployeeRoleUpdateDTO, EmployeeRole>();

            CreateMap<Role, RoleResponseDTO>();
            CreateMap<RoleCreateDTO, Role>();
            CreateMap<RoleUpdateDTO, Role>();
        }
    }
}

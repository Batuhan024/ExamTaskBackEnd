﻿using Business.Models.Request.Create;
using Business.Models.Request.Update;
using Business.Models.Response;
using Business.Services.Interface;
using Infrastructure.Data.Postgres.Entities;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.Base;

namespace ExamTask.Controllers
{
    public class EmployeeRoleController : BaseCRUDController<EmployeeRole, int, EmployeeRoleCreateDTO, EmployeeRoleUpdateDTO, EmployeeRoleResponseDTO>
    {
        public EmployeeRoleController(IEmployeeRoleService service) : base(service)
        {
        }
    }
}

using Business.Services.Base.Interface;
using Core.Results;
using Microsoft.AspNetCore.Mvc;
using Web.Filters;

namespace Web.Controllers.Base;

[ApiController]
[Route("api/[controller]/[action]")]
public class BaseController : ControllerBase
{
}

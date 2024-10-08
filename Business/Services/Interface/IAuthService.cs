using Business.Models.Request.Functional;
using Business.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Interface
{
    public interface IAuthService
    {
        LoginResponseDTO Authenticate(LoginRequestDTO request);
    }
}

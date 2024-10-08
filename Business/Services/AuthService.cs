using Business.Models.Request.Functional;
using Business.Models.Response;
using Business.Services.Interface;
using Infrastructure.Repositories.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class AuthService : IAuthService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IConfiguration _config;

        public AuthService(IEmployeeRepository employeeRepo, IConfiguration config)
        {
            _employeeRepository = employeeRepo;
            _config = config;
        }

        public LoginResponseDTO Authenticate(LoginRequestDTO request)
        {
            // Admin, Doctor ve Patient tablolarını kontrol et
            var employee = _employeeRepository.GetByUsername(request.Username);

            // Her bir tablo için kontrol işlemi
            if (employee != null && VerifyPassword(employee.Password, request.Password))
            {
                return CreateLoginResponse(employee.Email, employee.Id, employee.FullName, employee.DepartmentId);
            }

            // Kullanıcı bulunamadıysa null döneriz
            return null;
        }

        private bool VerifyPassword(string storedPassword, string enteredPassword)
        {
            // Şifreyi doğrula (hash ile de yapılabilir)
            return storedPassword == enteredPassword;
        }

        private LoginResponseDTO CreateLoginResponse(string email, int id, string fullname, int departmentid)
        {
            // Token oluştur
            var token = GenerateJwtToken(email, fullname);

            return new LoginResponseDTO
            {
                Token = token,
                Id = id,
                Name = fullname,
                Surname = departmentid,
                Role = email,
                Expiration = DateTime.UtcNow.AddHours(1) // Token 1 saat geçerli olacak
            };
        }

        private string GenerateJwtToken(string username, string role)
        {
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(ClaimTypes.Role, role),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
            
            //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySuperSecretKey12345MySuperSecretKey12345"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

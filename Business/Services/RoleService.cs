using Business.Models.Response;
using Business.Services.Base;
using Business.Services.Interface;
using Business.Utilities.Mapping.Interface;
using Core.Results; 
using Infrastructure.Data.Postgres.Entities;
using Infrastructure.Data.Postgres;
using System.Threading.Tasks;

namespace Business.Services
{
    public class RoleService : BaseService<Role, int, RoleResponseDTO>, IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoleService(IUnitOfWork unitOfWork, IMapperHelper mapperHelper)
            : base(unitOfWork, unitOfWork.Roles, mapperHelper)
        {
            _unitOfWork = unitOfWork;
        }



        // Yeni bir rol oluşturma işlemi rol ismi var mı kontrolü
        public async Task<Result> CreateRoleAsync(Role role)
        {
            var roleExists = await _unitOfWork.Roles.AnyAsync(r => r.Name == role.Name);
            if (roleExists)
            {
                return Result.Failure("Bu rol adı zaten kayıtlı.");
            }

            await _unitOfWork.Roles.AddAsync(role);
            await _unitOfWork.CommitAsync(); 

            return Result.Success();
        }
    }
}

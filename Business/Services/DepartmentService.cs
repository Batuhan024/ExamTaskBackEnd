using Business.Models.Response;
using Business.Services.Base;
using Business.Services.Interface;
using Business.Utilities.Mapping.Interface;
using Core.Results; // Result sınıfı
using Infrastructure.Data.Postgres;
using Infrastructure.Data.Postgres.Entities;
using System.Threading.Tasks;

namespace Business.Services
{
    public class DepartmentService : BaseService<Department, int, DepartmentResponseDTO>, IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentService(IUnitOfWork unitOfWork, IMapperHelper mapperHelper)
            : base(unitOfWork, unitOfWork.Departments, mapperHelper)
        {
            _unitOfWork = unitOfWork;
        }

        // Yeni bir departman ekleme işlemi isim kontrollü
        public async Task<Result> CreateDepartmentAsync(Department department)
        {
            var departmentExists = await _unitOfWork.Departments.AnyAsync(d => d.Name == department.Name);
            if (departmentExists)
            {
                return Result.Failure("Bu departman adı zaten kayıtlı.");
            }
            await _unitOfWork.Departments.AddAsync(department);
            await _unitOfWork.CommitAsync();

            return Result.Success();
        }
    }
}

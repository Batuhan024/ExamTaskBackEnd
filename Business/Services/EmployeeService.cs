using Business.Models.Response;
using Business.Services.Base;
using Business.Services.Interface;
using Business.Utilities.Mapping.Interface;
using Core.Results;
using Infrastructure.Data.Postgres;
using Infrastructure.Data.Postgres.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Business.Services
{
    public class EmployeeService : BaseService<Employee, int, EmployeeResponseDTO>, IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<EmployeeService> _logger;
        private readonly IMemoryCache _cache;

        public EmployeeService(ILogger<EmployeeService> logger, IMemoryCache cache, IUnitOfWork unitOfWork, IMapperHelper mapperHelper)
            : base(unitOfWork, unitOfWork.Employees, mapperHelper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _cache = cache;
        }


        //Email boş mu ve var mı kontrol metodu
        //StopWatch ile performans ölçümü yapıyorum ve loglayarak kontrolünü sağlıyorum.
        public async Task<Result> CreateEmployeeAsync(Employee employee)
        {
            var stopwatch = Stopwatch.StartNew();

            try
            {
                if (string.IsNullOrEmpty(employee.Email))
                {
                    return Result.Failure("Email adresi boş olamaz.");
                }

                var emailExists = await _unitOfWork.Employees.ExistsAsync(employee.Email);
                if (emailExists)
                {
                    return Result.Failure("Bu email adresi zaten kayıtlı.");
                }

                await _unitOfWork.Employees.AddAsync(employee);
                await _unitOfWork.CommitAsync();

                return Result.Success();
            }
            finally
            {
                stopwatch.Stop();
                _logger.LogInformation($"CreateEmployeeAsync metodu {stopwatch.ElapsedMilliseconds} ms sürdü.");
            }
        }

        //StopWatch ile performans ölçümü yapıyorum ve loglayarak kontrolünü sağlıyorum.
        public async Task<Result> UpdateEmployeeAsync(Employee employee)
        {
            var stopwatch = Stopwatch.StartNew(); 

            try
            {
                if (string.IsNullOrEmpty(employee.Email))
                {
                    return Result.Failure("Email adresi boş olamaz.");
                }

                var existingEmployee = await _unitOfWork.Employees.GetByIdAsync(employee.Id);
                if (existingEmployee == null)
                {
                    return Result.Failure("Güncellenmek istenen çalışan bulunamadı.");
                }

                var emailExists = await _unitOfWork.Employees.ExistsAsync(employee.Email);
                if (emailExists && existingEmployee.Email != employee.Email)
                {
                    return Result.Failure("Bu email adresi zaten kayıtlı.");
                }

                existingEmployee.FullName = employee.FullName;
                existingEmployee.Email = employee.Email;
                existingEmployee.Phone = employee.Phone;
                existingEmployee.DepartmentId = employee.DepartmentId;

                _unitOfWork.Employees.Update(existingEmployee);
                await _unitOfWork.CommitAsync();

                return Result.Success();
            }
            finally
            {
                stopwatch.Stop();
                _logger.LogInformation($"UpdateEmployeeAsync metodu {stopwatch.ElapsedMilliseconds} ms sürdü.");
            }
        }


        //StopWatch ile performans ölçümü yapıyorum ve ın memory cache loglayarak kontrolünü sağlıyorum.
        //İlk çalıştırmada veritabanından çektiği için ve ikinci çalıştırmada cacheden çektiği için
        // aradaki zaman farkı bariz bir şekilde loglarda görülebiliyor.
        public async Task<List<EmployeeResponseDTO>> GetEmployeesByRoleNameAsync(string roleName)
        {
            var stopwatch = Stopwatch.StartNew();
            var cacheKey = $"employees_role_{roleName}"; 

            try
            {
                // Cache'de var mı kontrol et
                if (!_cache.TryGetValue(cacheKey, out List<EmployeeResponseDTO> employeeDtos))
                {
                    _logger.LogInformation("Cache'de bulunamadı, veritabanından veri alınıyor...");

                    var role = await _unitOfWork.Roles.FirstOrDefaultAsync(r => r.Name == roleName);

                    if (role == null)
                    {
                        throw new ArgumentException("Böyle bir rol yok.");
                    }

                    var employeeRoles = await _unitOfWork.EmployeeRoles.GetAllAsync(er => er.RoleID == role.Id);

                    var employeeIds = employeeRoles.Where(er => !er.IsDeleted).Select(er => er.EmployeeID).ToList();

                    var employees = await _unitOfWork.Employees.GetAllAsync(e => employeeIds.Contains(e.Id));

                    employeeDtos = employees.Select(e => _mapperHelper.Map<EmployeeResponseDTO>(e)).ToList();

                    // Veriyi cache'e ekle ve logla
                    _cache.Set(cacheKey, employeeDtos, TimeSpan.FromMinutes(5));
                    _logger.LogInformation("Veriler cache'e eklendi.");
                }
                else
                {
                    _logger.LogInformation("Veriler cache'den alındı.");
                }

                return employeeDtos;
            }
            finally
            {
                stopwatch.Stop();
                _logger.LogInformation($"GetEmployeesByRoleNameAsync metodu {stopwatch.ElapsedMilliseconds} ms sürdü.");
            }
        }





        //In-memory cache kullanarak verileri cacheden çekiyorum. Loglar ile kontrolünü sağlıyorum. 
        public async Task<List<EmployeeResponseDTO>> GetEmployeesByDepartmentNameAsync(string departmentName)
        {
            var cacheKey = $"employees_department_{departmentName}"; // Cache anahtarı oluştur

            if (!_cache.TryGetValue(cacheKey, out List<EmployeeResponseDTO> employeeDtos))
            {
                _logger.LogInformation("Cache'de bulunamadı, veritabanından veri alınıyor...");

                var department = await _unitOfWork.Departments.FirstOrDefaultAsync(d => d.Name == departmentName);
                if (department == null)
                {
                    throw new ArgumentException("Böyle bir departman yok.");
                }

                var employees = await _unitOfWork.Employees.GetAllAsync(e => e.DepartmentId == department.Id && !e.IsDeleted);
                employeeDtos = employees.Select(e => _mapperHelper.Map<EmployeeResponseDTO>(e)).ToList();

                // Cache'e ekle ve logla
                _cache.Set(cacheKey, employeeDtos, TimeSpan.FromMinutes(5));
                _logger.LogInformation("Veriler cache'e eklendi.");
            }
            else
            {
                // Cache'den veri alındı, logla
                _logger.LogInformation("Veriler cache'den alındı.");
            }

            return employeeDtos; 
        }






        //StopWatch ile performans ölçümü yapıyorum ve loglayarak kontrolünü sağlıyorum.
        public async Task<Result> AssignRolesToEmployeeAsync(int adminUserId, int employeeId, List<int> roleIds)
        {
            var stopwatch = Stopwatch.StartNew();

            //Sadece adminler(IsAdmin = true) ekleme yapabilmesi için kontrol
            var adminRoles = await _unitOfWork.EmployeeRoles.GetAllAsync(er => er.EmployeeID == adminUserId);


            var adminRoleIds = adminRoles.Select(er => er.RoleID).ToList();
            var adminRoleEntities = await _unitOfWork.Roles.GetAllAsync(r => adminRoleIds.Contains(r.Id) && r.IsAdmin);

            if (!adminRoleEntities.Any())
            {
                return Result.Failure("Bu işlemi gerçekleştirmek için yetkiniz yok. Sadece admin kullanıcılar rol atayabilir.");
            }

            // Çalışan var mı kontrol ediyoruz
            var employee = await _unitOfWork.Employees.GetByIdAsync(employeeId);
            if (employee == null)
            {
                return Result.Failure("Atanacak çalışan bulunamadı.");
            }

            // Atanan roller var mı kontrol ediyoruz
            var validRoles = await _unitOfWork.Roles.GetAllAsync(r => roleIds.Contains(r.Id));
            if (validRoles.Count != roleIds.Count)
            {
                return Result.Failure("Atanan rollerden biri veya birkaçı geçerli değil.");
            }


            foreach (var roleId in roleIds)
            {
                var employeeRole = new EmployeeRole
                {
                    EmployeeID = employeeId,
                    RoleID = roleId
                };
                await _unitOfWork.EmployeeRoles.AddAsync(employeeRole);
            }

            await _unitOfWork.CommitAsync();

            stopwatch.Stop();
            _logger.LogInformation($"AssignRolesToEmployeeAsync metodu {stopwatch.ElapsedMilliseconds} ms sürdü.");

            return Result.Success("Roller başarıyla atandı.");
        }

    }
}

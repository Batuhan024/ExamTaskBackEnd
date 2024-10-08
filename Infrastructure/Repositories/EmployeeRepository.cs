using Infrastructure.Data.Postgres.EntityFramework;
using Infrastructure.Data.Postgres.Repositories.Base;
using Infrastructure.Data.Postgres.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Data.Postgres.Entities;
using Infrastructure.Repositories.Interface;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class EmployeeRepository : Repository<Employee, int>, IEmployeeRepository
    {
        private readonly PostgresContext _context;

        public EmployeeRepository(PostgresContext postgresContext) : base(postgresContext)
        {
            _context = postgresContext;
        }

        // Kullanıcı adına göre Admin döndür
        public Employee GetByUsername(string username)
        {
            return _context.Employees.SingleOrDefault(admin => admin.Email == username);
        }

        // Admin'in veritabanında mevcut olup olmadığını kontrol et (gerekirse)
        public async Task<bool> ExistsAsync(string email)
        {
            return await _context.Employees.AnyAsync(admin => admin.Email == email);
        }


        // Employee sorgulamak için IQueryable döndüren metot
        public IQueryable<Employee> GetAllEmployees()
        {
            return _context.Employees.AsQueryable();
        }

    }
}

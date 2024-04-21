using Microsoft.EntityFrameworkCore;
using MvcAppBLL.Interfaces;
using MvcAppDAL.Context;
using MvcAppDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcAppBLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {


        private readonly MvcAppDbContext _dbContext;

        public GenericRepository(MvcAppDbContext mvcAppDbContext)
        {
            _dbContext = mvcAppDbContext;
        }

        public async Task AddAsync(T item)
        {
            await _dbContext.AddAsync(item);
        }

        public  void Delete(T item)
        {
              _dbContext.Remove(item);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Employee))
            {
                return  (IEnumerable<T>) await _dbContext.Employees.Include(E=>E.department).ToListAsync();
            }
            return await _dbContext.Set<T>().ToListAsync();

        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public void Update(T item)
        {
            _dbContext.Update(item); 
        }
    }
}

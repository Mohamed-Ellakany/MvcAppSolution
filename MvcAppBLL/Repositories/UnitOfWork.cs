using MvcAppBLL.Interfaces;
using MvcAppDAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcAppBLL.Repositories
{
    public class UnitOfWork : IUnitOfWork ,IDisposable
    {
        private readonly MvcAppDbContext _dbContext;

        public IDepartmentRepository DepartmentRepository { get; set; }

        public IEmployeeRepository EmployeeRepository { get; set; }



        public UnitOfWork(MvcAppDbContext dbContext)
        {
            DepartmentRepository =new DepartmentRepository(dbContext);
            EmployeeRepository =new EmployeeRepository(dbContext);
            _dbContext = dbContext;
        }

        public async Task<int> CompleteAsync()
        {
           return await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}

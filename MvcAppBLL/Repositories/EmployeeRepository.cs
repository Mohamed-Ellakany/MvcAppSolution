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
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly MvcAppDbContext _mvcAppDbContext;

        public EmployeeRepository(MvcAppDbContext mvcAppDbContext):base(mvcAppDbContext) 
        {
            _mvcAppDbContext = mvcAppDbContext;
        }


        public  IQueryable<Employee> Search(string Name)
        {
          return _mvcAppDbContext.Employees.Where(e => e.Name.Trim().ToLower().Contains(Name.ToLower().Trim()));
        }

        
    }
}

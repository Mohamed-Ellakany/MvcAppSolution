using MvcAppDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcAppBLL.Interfaces
{
    public interface IEmployeeRepository :IGenericRepository<Employee>
    {

        IQueryable<Employee> Search(string Name); 


    }
}

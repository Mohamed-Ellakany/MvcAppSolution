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
    public class DepartmentRepository :GenericRepository<Department> , IDepartmentRepository
    {

        public DepartmentRepository(MvcAppDbContext mvcAppDbContext):base(mvcAppDbContext)
        {
        }



    }
}

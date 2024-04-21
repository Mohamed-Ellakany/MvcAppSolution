using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MvcAppDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcAppDAL.Context
{
    public class MvcAppDbContext : IdentityDbContext <AppUser>
    {
         
        public MvcAppDbContext(DbContextOptions<MvcAppDbContext> options) : base(options)
        {
       
        }

            public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
      

       
    }
    
}


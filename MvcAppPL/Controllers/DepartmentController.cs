using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MvcAppBLL.Interfaces;
using MvcAppDAL.Models;
using System;
using System.Threading.Tasks;

namespace MvcAppPL.Controllers
{

    [Authorize]
    public class DepartmentController : Controller
    {
        
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentController(IUnitOfWork unitOfWork)
        {
           
           _unitOfWork = unitOfWork;
        }

        //base/department/index
        public async Task<IActionResult> Index()
        {
            var departments =await _unitOfWork.DepartmentRepository.GetAllAsync();
            
            return View(departments);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Department department)
        {
            if(ModelState.IsValid)
            {
               await _unitOfWork.DepartmentRepository.AddAsync(department);
               await _unitOfWork.CompleteAsync();
                return RedirectToAction(nameof(Index));
            }
           return View();
        }


        public async Task<IActionResult> Details(int? id ,string ViewName ="Details")
        {
            if(id is null )
                 return BadRequest();
            var department =await _unitOfWork.DepartmentRepository.GetByIdAsync(id.Value);
            if (department is null)
                 return NotFound();
                
            return View(ViewName,department);
        }

        [HttpGet]
        public Task<IActionResult> Edit(int? id)
        {
            //if (id is null)
            //    return BadRequest();
            //var department = _departmentRepository.GetById(id.Value);
            //if (department is null)
            //    return NotFound();

            //return View(department);
            return Details(id ,"Edit");

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Department department ,[FromRoute] int id )
        {
            if(id != department.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.DepartmentRepository.Update(department);
                   await  _unitOfWork.CompleteAsync();

                    return RedirectToAction(nameof(Index));
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError(string.Empty ,ex.Message);
                }
              
            }
            return View(department);
        }


        public Task<IActionResult> Delete(int? id)
        {
            return Details(id, "Delete");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Department department, [FromRoute] int id)
        {
            if (id != department.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.DepartmentRepository.Delete(department);
                   await _unitOfWork.CompleteAsync();

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

            }
            return RedirectToAction(nameof(Index));
        }
    }
}

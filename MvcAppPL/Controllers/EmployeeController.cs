using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MvcAppBLL.Interfaces;
using MvcAppBLL.Repositories;
using MvcAppDAL.Models;
using MvcAppPL.Helpers;
using MvcAppPL.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace MvcAppPL.Controllers
{

    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeController(IUnitOfWork unitOfWork, IMapper mapper  )
        {
           _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public   async Task<IActionResult> Index(string SearchName = "")
        {
            IEnumerable<Employee> employees;

            if (string.IsNullOrEmpty(SearchName))
            {
                employees =await _unitOfWork.EmployeeRepository.GetAllAsync();
            }
            else
            {
                employees = _unitOfWork.EmployeeRepository.Search(SearchName);
            }

             var MappedEmployee = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);
            return View(MappedEmployee);
        }



        public IActionResult Create()
        {
           //ViewBag.Departments=_departmentRepository.GetAll();
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel employeeVM)
        {
            if (ModelState.IsValid)
            {
               
                employeeVM.ImageName= DocumentsSettings.UploadFile(employeeVM.Image, "Images"); 
                var MappedEmployee = _mapper.Map< EmployeeViewModel , Employee >(employeeVM);

               await _unitOfWork.EmployeeRepository.AddAsync(MappedEmployee);
               int res=await _unitOfWork.CompleteAsync();

                if (res > 0 )
                {
                    TempData["message"] = "Employee is Created";
                }
                else
                {
                    TempData["message"] = "Employee is not Created";
                }
                return RedirectToAction(nameof(Index));
            }
            return View();
        }


        public async Task<IActionResult> Details(int? id, string ViewName = "Details")
        {
            if (id is null)
                return BadRequest();
            var employee =await _unitOfWork.EmployeeRepository.GetByIdAsync(id.Value);
            if (employee is null)
                return NotFound();
            var mappedEmployee = _mapper.Map<Employee, EmployeeViewModel>(employee);
            return View(ViewName, mappedEmployee);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id )
        {
            TempData["CurrentImage"] = _unitOfWork.EmployeeRepository.GetByIdAsync((int)id).Result.ImageName;
            return await Details(id, "Edit");

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EmployeeViewModel employeeVM, [FromRoute] int id)
           
        {
            if (id != employeeVM.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
          
                {
                    if (employeeVM.Image is not null)
                    {
                        employeeVM.ImageName = DocumentsSettings.UploadFile(employeeVM.Image, "Images");
                    }
                    else
                    {
                        employeeVM.ImageName = TempData["CurrentImage"] as string;
                    }


                    var mappedEmployee = _mapper.Map<EmployeeViewModel,Employee >(employeeVM);
                    _unitOfWork.EmployeeRepository.Update(mappedEmployee);
                   await _unitOfWork.CompleteAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

            }
            return View(employeeVM);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(EmployeeViewModel employeeVM, [FromRoute] int id)
        {
            if (id != employeeVM.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                    
                {
                    var mappedEmployee = _mapper.Map<EmployeeViewModel,Employee >(employeeVM);
                    _unitOfWork.EmployeeRepository.Delete(mappedEmployee);
                   int res =await _unitOfWork.CompleteAsync();
                    if (res > 0 && employeeVM.ImageName is not null)
                    {
                        DocumentsSettings.Delete(employeeVM.ImageName, "Images");
                    }
                    
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

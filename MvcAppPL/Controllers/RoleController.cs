using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcAppDAL.Models;
using MvcAppPL.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MvcAppPL.Controllers
{
    [Authorize]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public RoleController(RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }


        public async Task<IActionResult> Index(string SearchName)
        {
            if (string.IsNullOrEmpty(SearchName))
            {
                var Roles = await _roleManager.Roles.ToListAsync();
                var mappedRole = _mapper.Map<IEnumerable<IdentityRole>, IEnumerable<RoleViewModel>>(Roles);
                return View(mappedRole);
            }
            else
            {
                var Roles = await _roleManager.FindByNameAsync(SearchName);
                var mappedRole = _mapper.Map<IdentityRole, RoleViewModel>(Roles);
                    return View(new List<RoleViewModel> { mappedRole });
            }
        }



        public IActionResult Create()
        {
             return  View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel model)
        {
            if(ModelState.IsValid)
            {
                var mappedRole = _mapper.Map<RoleViewModel, IdentityRole>(model);
                await _roleManager.CreateAsync(mappedRole);
                return RedirectToAction("Index");
            }
            return View(model);
        }






        public async Task<IActionResult> Details(string Id, string ViewName = "Details")
        {
            if (Id is null)
                return BadRequest();
            var Role = await _roleManager.FindByIdAsync(Id);
            if (Role is null) return NotFound();
            else
            {
                var MapperRole = _mapper.Map<IdentityRole, RoleViewModel>(Role);
                return View(ViewName, MapperRole);

            }



        }




        public async Task<IActionResult> Edit(string Id)
        {
            return await Details(Id, "Edit");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RoleViewModel model, [FromRoute] string Id)
        {
            if (Id != model.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var Role = await _roleManager.FindByIdAsync(Id);

                    Role.Name = model.RoleName;


                    //  var mappedUser = _mapper.Map<UserViewModel, AppUser>(UserVM);
                    await _roleManager.UpdateAsync(Role);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

            }
            return View(model);
        }




        public async Task<IActionResult> Delete([FromRoute] string Id)
        {
            return await Details(Id, "Delete");
        }


        [HttpPost]
        public async Task<IActionResult> ConfirmDelete(string Id)
        {
            try
            {
                var User = await _roleManager.FindByIdAsync(Id);
                await _roleManager.DeleteAsync(User);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToAction("Error", ex.Message);

            }

        }






    }
}

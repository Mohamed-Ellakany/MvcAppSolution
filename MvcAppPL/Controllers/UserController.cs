using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.EntityFrameworkCore;
using MvcAppDAL.Models;
using MvcAppPL.Helpers;
using MvcAppPL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcAppPL.Controllers
{

    [Authorize]
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public UserController(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }


        public async Task<IActionResult> Index(string SearchName)
        {

            if (string.IsNullOrEmpty(SearchName))
            {
                var Users = await _userManager.Users.Select(
                    U => new UserViewModel()
                    {
                        Id = U.Id,
                        Fname = U.Fname,
                        Lname = U.Lname,
                        Email = U.Email,
                        PhoneNumber = U.PhoneNumber,
                        Roles = _userManager.GetRolesAsync(U).Result
                    }).ToListAsync();
                return View(Users);
            }
            else
            {
                var User = await _userManager.FindByEmailAsync(SearchName);
                var MappedUser = new UserViewModel()
                {
                    Id = User.Id,
                    Fname = User.Fname,
                    Lname = User.Lname,
                    Email = User.Email,
                    PhoneNumber = User.PhoneNumber,
                    Roles = _userManager.GetRolesAsync(User).Result
                };
                return View(new List<UserViewModel> { MappedUser });


            }


            //P@ssw0rd

        }




        public async Task<IActionResult> Details(string Id, string ViewName = "Details")
        {
            if (Id is null)
                return BadRequest();
            var User = await _userManager.FindByIdAsync(Id);
            if (User is null) return NotFound();
            else
            {
                var MapperUser = _mapper.Map<AppUser, UserViewModel>(User);
                return View(ViewName, MapperUser);

            }



        }




        public async Task<IActionResult> Edit(string Id)
        {
            return await Details(Id, "Edit");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserViewModel UserVM, [FromRoute] string Id)
        {
            if (Id != UserVM.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.FindByIdAsync(Id);
                    
                    user.Fname = UserVM.Fname;
                    user.Lname = UserVM.Lname;
                    user.PhoneNumber = UserVM.PhoneNumber;


                  //  var mappedUser = _mapper.Map<UserViewModel, AppUser>(UserVM);
                    await _userManager.UpdateAsync(user);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

            }
            return View(UserVM);
        }


       
       /// ///////////
       

        public async Task<IActionResult> Delete([FromRoute ]string Id)
        {
            return await Details(Id, "Delete");
        }


        [HttpPost]
        public async Task<IActionResult> ConfirmDelete(string Id )
        {
            try
            {
                var User = await _userManager.FindByIdAsync(Id);
                await _userManager.DeleteAsync(User);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex) 
            {
                ModelState.AddModelError(string.Empty , ex.Message);
                return RedirectToAction("Error" , ex.Message);

            }

        }













    }




}

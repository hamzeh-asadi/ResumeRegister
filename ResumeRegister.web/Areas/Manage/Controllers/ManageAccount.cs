using Microsoft.AspNetCore.Mvc;
using ResumeRegister.core.DTOs;
using ResumeRegister.core.Security;
using ResumeRegister.core.Services.Interfaces;
using System.Collections.Generic;

namespace ResumeRegister.web.Areas.Manage.Controllers
{
    [Area("Manage"), PermissionChecker("SiteAdmin")]
    public class ManageAccount : Controller
    {
        private IAccountService _accountService;
        private IResumeService _resumeService;

        public ManageAccount(IAccountService accountService,IResumeService resumeService)
        {
            _accountService = accountService;
            _resumeService = resumeService;
        }

        [Route("Manage/AllUser")]
        public IActionResult AllUser()
        {
            var allUser = _accountService.GetAlluser();
            return View(allUser);
        }

        #region Add New User

        [Route("Manage/AddUser")]
        public IActionResult AddNewUser()
        {
            ViewData["AllRoles"] = _accountService.GetAllRoles();
            return View();
        }

        [Route("Manage/AddUser"), HttpPost]
        public IActionResult AddNewUser(AdminUserRegisterViewModel user, List<int> selectedRole)
        {
            ViewData["AllRoles"] = _accountService.GetAllRoles();
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "ثبت نام با موفقیت انجام نشد لطفا دوباره تلاش کنید .");
                return View(user);
            }
            if (_accountService.IsUserNameExist(user.UserName))
            {
                ModelState.AddModelError("UserName", "نام کاربری معتبر نمی باشد لطفا نام کاربری دیگری انتخاب کنید .");
                return View(user);
            }
            if (_accountService.IsEmailExist(user.Email))
            {
                ModelState.AddModelError("Email", "ایمیل معتبر نمی باشد لطفا نام ایمیل دیگری انتخاب کنید .");
                return View(user);
            }
            var userId = _accountService.AdminAddNewUser(user);
            TempData["AddUserSuccess"] = true;
            if (selectedRole != null)
            {
                _accountService.AddRoleToUser(selectedRole, userId);
            }
            return RedirectToAction("AddNewUser");
        }

        #endregion Add New User

        #region Delete User

        [Route("Manage/DeleteUser/{userId}")]
        public IActionResult DeleteUser(int userId)
        {
            var user = _accountService.GetUser(userId);
            return View(user);
        }

        [Route("Manage/DeleteUser/{userId}"), HttpPost]
        public IActionResult DeleteUser(int UserId, string UserName)
        {
            if (!ModelState.IsValid)
            {
                var user = _accountService.GetUser(UserId);
                ModelState.AddModelError("", " حذف با موفقیت انجام نشد لطفا دوباره تلاش کنید .");
                return View(user);
            }
            _accountService.DeleteRoleForUser(UserId);
            _resumeService.DeleteResumeForUser(UserId);
            _accountService.DeleteUser(UserId);
            return RedirectToAction("AllUser");
        }

        #endregion Delete User
        
        #region Edit User

        [Route("Manage/EditUser/{userId}")]
        public IActionResult EditUser(int userId)
        {
            var user = _accountService.GetUser(userId);
            var userRoles = _accountService.GetRolesForUser(userId);
            AdminUserEditViewModel userEdit = new AdminUserEditViewModel()
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Email = user.Email,
                FirstNameUser = user.FirstNameUser,
                IsActive = user.IsActive,
                LastNameUser = user.LastNameUser,
                RoleIds = userRoles
            };
            ViewData["AllRoles"] = _accountService.GetAllRoles();
            return View(userEdit);
        }

        [Route("Manage/EditUser/{userId}"), HttpPost]
        public IActionResult EditUser(List<int> selectedRole,AdminUserEditViewModel user)
        {
            ViewData["AllRoles"] = _accountService.GetAllRoles();
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", " ویرایش با موفقیت انجام نشد لطفا دوباره تلاش کنید .");
                return View(user);
            }
            
            _accountService.EditRolesForUser(selectedRole,user.UserId);
            _accountService.AdminEditUser(user);
            return RedirectToAction("AllUser");
        }

        #endregion Delete User

    }
}
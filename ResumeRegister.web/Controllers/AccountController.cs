using DNTCaptcha.Core;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResumeRegister.core.DTOs;
using ResumeRegister.core.Services.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;

namespace ResumeRegister.web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IResumeService _resumeService;

        public AccountController(IAccountService account, IResumeService resumeService)
        {
            _accountService = account;
            _resumeService = resumeService;
        }

        #region User

        [Route("Register"), AllowAnonymous]
        public IActionResult RegisterUser()
        {
            return View();
        }

        [Route("Register"), HttpPost, AllowAnonymous]
        [ValidateAntiForgeryToken]
        [ValidateDNTCaptcha(
            ErrorMessage = "کد امنیتی را مجدد وارد کنید",
            CaptchaGeneratorLanguage = Language.English,
            CaptchaGeneratorDisplayMode = DisplayMode.ShowDigits)]
        public IActionResult RegisterUser(UserRegisterViewModel user)
        {
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
            var userId = _accountService.AddNewUser(user);
            TempData["AddUserSuccess"] = true;
            return RedirectToAction("login", "Account");
        }

        [Route("EditUser/userId")]
        public IActionResult EditUser(int userId)
        {
            var user = _accountService.GetUser(userId);
            if (!user.IsActive)
            {
                return RedirectToAction("Logout");
            }
            UserEditViewModel editUser = new UserEditViewModel()
            {
                UserId = user.UserId,
                Email = user.Email,
                FirstNameUser = user.FirstNameUser,
                LastNameUser = user.LastNameUser,
                UserName = user.UserName
            };
            return View(editUser);
        }

        [Route("EditUser/userId"), HttpPost]
        public IActionResult EditUser(UserEditViewModel editUser)
        {
            var user = _accountService.GetUser(editUser.UserId);
            if (!user.IsActive)
            {
                return RedirectToAction("Logout");
            }
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "ثبت نام با موفقیت انجام نشد لطفا دوباره تلاش کنید .");
                return View(editUser);
            }

            if (string.IsNullOrEmpty(editUser.Password))
            {
                editUser.Password = user.Password;
            }
            _accountService.EditUser(editUser);
            TempData["EditUserSuccess"] = true;
            return View(editUser);
        }

        #endregion User

        #region Resume

        #region Register New Resume

        [Route("ResumeRegister/{userId}")]
        public IActionResult ResumeRegister(int userId)
        {
            var user = _accountService.GetUser(userId);
            if (!user.IsActive)
            {
                return RedirectToAction("Logout");
            }
            if (_resumeService.IsResumeExistWithUserId(userId))
            {
                return Redirect($"/EditResume/{userId}");
            }
            ViewData["UserData"] = user;
            return View();
        }

        [Route("ResumeRegister/{userId}"), HttpPost]
        public IActionResult ResumeRegister(ResumeRegisterViewModel newResume)
        {
            var user = _accountService.GetUser(newResume.UserId);
            ViewData["UserData"] = user;
            if (!user.IsActive)
            {
                return RedirectToAction("Logout");
            }
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "ثبت نام با موفقیت انجام نشد لطفا دوباره تلاش کنید .");
                return View(newResume);
            }

            if (newResume.Resume != null)
            {
                //todo check file size
                if (newResume.Resume.Length > 20971520)
                {
                    ModelState.AddModelError("", "حداکثر حجم مجاز برای فایل 20 مگابایت (MB)  است .");
                    return View(newResume);
                }
                var resumeExt = Path.GetExtension(newResume.Resume.FileName).ToLower();
                if (resumeExt != ".pdf")
                {
                    ModelState.AddModelError("", "لطفا یک فایل با پسوند PDF انتخاب کنید .");
                    return View(newResume);
                }

                string fileName = user.UserId + "_" + user.FullName + resumeExt;
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    newResume.Resume.CopyTo(stream);
                }

                newResume.ResumeName = fileName;
            }
            else
            {
                ModelState.AddModelError("", "لطفا یک فایل با پسوند PDF انتخاب کنید .");
                return View(newResume);
            }
            if (newResume.Picture != null)
            {
                //todo check file size
                var imgExt = Path.GetExtension(newResume.Picture.FileName).ToLower();
                var imgType = newResume.Picture.ContentType.ToLower();
                if (imgType != "image/jpg" && imgType != "image/jpeg" && imgType != "image/pjpeg" && imgType != "image/gif"
                    && imgType != "image/x-png" && imgType != "image/png" && imgType != "image/bmp")
                {
                    ModelState.AddModelError("", "لطفا یک فایل تصویر انتخاب کنید .");
                    return View(newResume);
                }

                string fileName = user.UserId + "_" + user.FullName + imgExt;
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files\\Images", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    newResume.Picture.CopyTo(stream);
                }

                newResume.PictureName = fileName;
            }
            else
            {
                ModelState.AddModelError("", "لطفا یک فایل تصویر انتخاب کنید .");
                return View(newResume);
            }
            _resumeService.AddNewResume(newResume);
            return RedirectToAction("SuccessRegister");
        }

        #endregion Register New Resume

        #region Edit Resume

        [Route("EditResume/{userId}")]
        public IActionResult EditResume(int userId)
        {
            var user = _accountService.GetUser(userId);
            ViewData["UserData"] = user;
            if (!user.IsActive)
            {
                return RedirectToAction("Logout");
            }
            var resume = _resumeService.GetResumeByUserId(userId);
            ResumeEditViewModel editResume = new ResumeEditViewModel()
            {
                ResumeId = resume.ResumeId,
                WorkBackground = resume.WorkBackground,
                UserId = resume.UserId,
                Expertise = resume.Expertise,
                MajorStudy = resume.MajorStudy,
                MinorStudy = resume.MinorStudy,
                Phone = resume.Phone,
                ResumeName = resume.ResumeName,
                CityOfBirth = resume.CityOfBirth,
                ProvinceOfBirth = resume.ProvinceOfBirth,
                University = resume.University,
                ProvinceOfResidence = resume.ProvinceOfResidence,
                CityOfResidence = resume.CityOfResidence,
                PictureName = resume.PictureName,
                Birthday = resume.Birthday,
            };
            return View(editResume);
        }

        [Route("EditResume/{userId}"), HttpPost]
        public IActionResult EditResume(ResumeEditViewModel resume, IFormFile resumeFile, IFormFile imgInput)
        {
            var user = _accountService.GetUser(resume.UserId);
            ViewData["UserData"] = user;
            if (!user.IsActive)
            {
                return RedirectToAction("Logout");
            }
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "ثبت نام با موفقیت انجام نشد لطفا دوباره تلاش کنید .");
                return View(resume);
            }

            if (resumeFile != null)
            {
                //todo check file size
                if (resumeFile.Length > 20971520)
                {
                    ModelState.AddModelError("", "حداکثر حجم مجاز برای فایل 20 مگابایت (MB)  است .");
                    return View(resume);
                }

                var resumeExt = Path.GetExtension(resumeFile.FileName).ToLower();
                string fileName = user.UserId + "_" + user.FullName + resumeExt;

                if (resumeExt != ".pdf")
                {
                    ModelState.AddModelError("", "لطفا یک فایل با پسوند PDF انتخاب کنید .");
                    return View(resume);
                }

                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", fileName);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    resumeFile.CopyTo(stream);
                }

                resume.ResumeName = fileName;
            }

            if (imgInput != null)
            {
                //todo check file size
                var imgExt = Path.GetExtension(imgInput.FileName).ToLower();
                var imgType = imgInput.ContentType.ToLower();
                if (imgType != "image/jpg" && imgType != "image/jpeg" && imgType != "image/pjpeg" && imgType != "image/gif"
                    && imgType != "image/x-png" && imgType != "image/png" && imgType != "image/bmp")
                {
                    ModelState.AddModelError("", "لطفا یک فایل تصویر انتخاب کنید .");
                    return View(resume);
                }

                string fileName = user.UserId + "_" + user.FullName + imgExt;
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files\\Images", fileName);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    imgInput.CopyTo(stream);
                }

                resume.PictureName = fileName;
            }

            _resumeService.UpdateResume(resume);
            return RedirectToAction("SuccessRegister");
        }

        #endregion Edit Resume

        public IActionResult SuccessRegister()
        {
            return View();
        }

        #endregion Resume

        #region Login/Logout

        [Route("Login"), AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [Route("Login"), HttpPost, AllowAnonymous]
        [ValidateAntiForgeryToken]
        [ValidateDNTCaptcha(
            ErrorMessage = "کد امنیتی را مجدد وارد کنید",
            CaptchaGeneratorLanguage = Language.English,
            CaptchaGeneratorDisplayMode = DisplayMode.ShowDigits)]
        public IActionResult Login(LoginViewModel loginInfo)
        {
            if (!ModelState.IsValid)
            {
                return View(loginInfo);
            }

            var user = _accountService.LoginUserInfo(loginInfo);
            if (user != null && user.IsActive)
            {
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
                    new Claim(ClaimTypes.Name,user.UserName),
                    new Claim(ClaimTypes.GivenName,user.FullName)
                };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                var properties = new AuthenticationProperties()
                {
                    IsPersistent = loginInfo.RememberMe
                };
                HttpContext.SignInAsync(principal, properties);
                return RedirectToAction("Index", "Home", user.UserId);
            }
            ModelState.AddModelError("UserName", "کاربری با مشخصات وارد شده یافت نشد یا کاربر غیرفعال است !!!");
            return View(loginInfo);
        }

        [Route("Logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/Login");
        }

        #endregion Login/Logout

        [Route("Download/{resumeName}")]
        public IActionResult DownloadResume(string resumeName)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files", resumeName);
            if (!string.IsNullOrEmpty(path))
            {
                byte[] file = System.IO.File.ReadAllBytes(path);
                return File(file, "application/force-download", resumeName);
            }

            return Content("فایل درخواستی موجود نمیباشد");
        }
    }
}
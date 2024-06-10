using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ResumeRegister.core.DTOs;
using ResumeRegister.core.Security;
using ResumeRegister.core.Services.Interfaces;

namespace ResumeRegister.web.Areas.Manage.Controllers
{
    [Area("Manage"), PermissionChecker("SiteAdmin")]
    public class ManegeResume : Controller
    {
        private readonly IResumeService _resumeService;
        private readonly IAccountService _accountService;

        public ManegeResume(IResumeService resumeService, IAccountService accountService)
        {
            _resumeService = resumeService;
            _accountService = accountService;
        }

        [Route("Manage/Detail/{resumeId}")]
        public IActionResult ResumeInfo(int resumeId)
        {
            var resume = _resumeService.GetResumeDetailForShow(resumeId);

            return View(resume);
        }

        [Route("Manage/Detail/{resumeId}"), HttpPost]
        public IActionResult ResumeInfo(int resumeId, bool IsChecked, bool IsAccepted)
        {
            var resume = _resumeService.GetResumeDetailForShow(resumeId);
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "عملیات با موفقیت انجام نشد لطفا دوباره تلاش کنید .");
                return View(resume);
            }
            _resumeService.UpdateResumeForAcceptedChecked(resumeId, IsChecked, IsAccepted);
            return RedirectToAction("index", "ManageHome");
        }

        [Route("Manage/Checked")]
        public IActionResult ResumeChecked()
        {
            var resume = _resumeService.CheckedResume();
            ViewData["AllUser"] = _accountService.GetAlluser().Select(u => new UserFullNameAndIdViewModel()
            {
                FullName = u.FullName,
                UserId = u.UserId
            }).ToList();
            return View(resume);
        }

        [Route("Manage/Accepted")]
        public IActionResult ResumeAccepted()
        {
            var resume = _resumeService.AcceptedResume();
            ViewData["AllUser"] = _accountService.GetAlluser().Select(u => new UserFullNameAndIdViewModel()
            {
                FullName = u.FullName,
                UserId = u.UserId
            }).ToList();
            return View(resume);
        }

        [Route("Manage/Download/{resumeName}")]
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

        [Route("Manage/AdvancedSearch")]
        public IActionResult AdvancedSearch()
        {
            List<AdvancedSearchViewModel> emptyModel = null;
            return View(emptyModel);
        }
        [Route("Manage/AdvancedSearch"), HttpPost]
        public IActionResult AdvancedSearch(string isChecked = "", string isAccepted = "", string name = "", string email = "", string phone = "", string expertise = "",
            string majorStudy = "", string minorStudy = "", int minBackground = -1, int maxBackground = -1, string university = "",
            string birthLocation = "", string residenceLocation = "")
        {
            var result = _resumeService.AdvancedSearch(isChecked, isAccepted, name, email, phone,
                expertise, majorStudy, minorStudy, minBackground, maxBackground, university, birthLocation, residenceLocation);
            return View(result);
        }
    }
}
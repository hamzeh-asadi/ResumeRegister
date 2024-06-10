using Microsoft.AspNetCore.Mvc;
using System.Linq;
using ResumeRegister.core.DTOs;
using ResumeRegister.core.Security;
using ResumeRegister.core.Services.Interfaces;

namespace ResumeRegister.web.Areas.Manage.Controllers
{
    [Area("Manage"),PermissionChecker("SiteAdmin")]
    public class ManageHome : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IResumeService _resumeService;

        public ManageHome(IAccountService accountService, IResumeService resumeService)
        {
            _accountService = accountService;
            _resumeService = resumeService;
        }
        

        [Route("manage/")]
        [Route("manage/index")]
        public IActionResult Index()
        {
            ViewData["AllUser"] = _accountService.GetAlluser().Select(u => new UserFullNameAndIdViewModel()
            {
                FullName = u.FullName,
                UserId = u.UserId
            }).ToList();
            var resumeList = _resumeService.GetAllResume();
            return View(resumeList);
        }
    }
}
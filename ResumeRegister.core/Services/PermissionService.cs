using System.Collections.Generic;
using System.Linq;
using ResumeRegister.core.Services.Interfaces;
using ResumeRegister.datalayer.Context;

namespace ResumeRegister.core.Services
{
    public class PermissionService:IPermissionService
    {
        private readonly ResumeRegisterContext _context;

        public PermissionService(ResumeRegisterContext context)
        {
            _context = context;
        }
        public bool CheckPermission(string enPermission, string userName)
        {
            int userId = _context.UserInfos.Single(u => u.UserName == userName).UserId;
            int permissionId = _context.Permissions.Single(p => p.EnPermissionTitle == enPermission).PermissionId;
            List<int> userRoles = _context.UserRoles.Where(r => r.UserId == userId).Select(r => r.RoleId).ToList();
            if (!userRoles.Any())
            {
                return false;
            }

            List<int> rolePermission = _context.RolePermissions.Where(p => p.PermissionId == permissionId)
                .Select(p => p.RoleId).ToList();
            return rolePermission.Any(p => userRoles.Contains(p));
        }

        public bool IsUserAdmin(string userName)
        {
            var userId = _context.UserInfos.Single(u => u.UserName == userName).UserId;
            var roleId = _context.UserRoles.Where(u => u.UserId == userId).Select(r => r.RoleId).ToList();
            return roleId.Contains(1);
        }
    }
}

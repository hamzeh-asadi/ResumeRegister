using ResumeRegister.core.Convertors;
using ResumeRegister.core.DTOs;
using ResumeRegister.core.Services.Interfaces;
using ResumeRegister.datalayer.Context;
using ResumeRegister.datalayer.Entities.Users;
using ResumeRegister.core.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ResumeRegister.core.Services
{
    public class AccountService : IAccountService
    {
        private ResumeRegisterContext _context;

        public AccountService(ResumeRegisterContext context)
        {
            _context = context;
        }

        public int AddNewUser(UserRegisterViewModel user)
        {
            var newUser = new UserInfo()
            {
                UserName = user.UserName,
                FirstNameUser = user.FirstNameUser,
                LastNameUser = user.LastNameUser,
                Password = user.Password,
                Email = user.Email,
                IsActive = true,
                RegisterDate = DateTime.Now
            };
            _context.UserInfos.Add(newUser);
            _context.SaveChanges();
            return newUser.UserId;
        }

        public void EditUser(UserEditViewModel user)
        {
            var editUser = GetUser(user.UserId);
            editUser.FirstNameUser = user.FirstNameUser;
            editUser.LastNameUser = user.LastNameUser;
            editUser.Password = user.Password;
            _context.UserInfos.Update(editUser);
            _context.SaveChanges();
        }

        public int AdminAddNewUser(AdminUserRegisterViewModel user)
        {
            var newUser = new UserInfo()
            {
                UserName = user.UserName,
                FirstNameUser = user.FirstNameUser,
                LastNameUser = user.LastNameUser,
                Password = user.Password,
                Email = user.Email,
                IsActive = user.IsActive,
                RegisterDate = DateTime.Now
            };
            _context.UserInfos.Add(newUser);
            _context.SaveChanges();
            return newUser.UserId;
        }

        public int AdminEditUser(AdminUserEditViewModel user)
        {
            var editUser = GetUser(user.UserId);
            editUser.Email = user.Email;
            editUser.FirstNameUser = user.FirstNameUser; 
            editUser.IsActive = user.IsActive;
            editUser.LastNameUser = user.LastNameUser;
            editUser.UserName = user.UserName;
            if (!string.IsNullOrEmpty(user.Password))
            {
                editUser.Password = user.Password;
            }
            _context.UserInfos.Update(editUser);
            _context.SaveChanges();
            return editUser.UserId;
        }

        public bool IsUserNameExist(string userName)
        {
            return _context.UserInfos.Any(u => u.UserName == userName);
        }

        public bool IsEmailExist(string email)
        {
            return _context.UserInfos.Any(u => u.Email == email);
        }

        public UserInfo LoginUserInfo(LoginViewModel user)
        {
            var userName = TextAndDateConvertor.TrimAndLower(user.UserName);
            var result = _context.UserInfos.SingleOrDefault(u => u.UserName == userName && u.Password == user.Password);
            if (result == null)
            {
                result = _context.UserInfos.SingleOrDefault(u => u.Email == userName && u.Password == user.Password);
            }

            return result;
        }

        public UserInfo GetUser(int userId)
        {
            return _context.UserInfos.Find(userId);
        }

        public UserInfo GetUser(string userName)
        {
            return _context.UserInfos.SingleOrDefault(u => u.UserName == userName);
        }

        public List<UserInfo> GetAlluser()
        {
            return _context.UserInfos.ToList();
        }

        public bool IsUserActive(int userId)
        {
            var user = GetUser(userId);
            return user.IsActive;
        }

        public void DeleteUser(int userId)
        {
            var user = GetUser(userId);
            _context.UserInfos.Remove(user);
            _context.SaveChanges();
        }

        public List<Role> GetAllRoles()
        {
            return _context.Roles.ToList();
        }

        public void AddRoleToUser(List<int> roleIds, int userId)
        {
            foreach (var roleId in roleIds)
            {
                UserRole userRole = new UserRole()
                {
                    RoleId = roleId,
                    UserId = userId
                };
                _context.UserRoles.Add(userRole);
            }
            _context.SaveChanges();
        }

        public void DeleteRoleForUser(int userId)
        {
            List<UserRole> userRoles = _context.UserRoles.Where(r => r.UserId == userId).ToList();
            foreach (var userRole in userRoles)
            {
                _context.UserRoles.Remove(userRole);
            }
            _context.SaveChanges();
        }

        public List<int> GetRolesForUser(int userId)
        {
            return _context.UserRoles.Where(u => u.UserId == userId).Select(r => r.RoleId).ToList();
        }

        public void EditRolesForUser(List<int> roles, int userId)
        {
            DeleteRoleForUser(userId);
            AddRoleToUser(roles,userId);
        }
    }
}
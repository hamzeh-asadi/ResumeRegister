using System.Collections.Generic;
using ResumeRegister.core.DTOs;
using ResumeRegister.datalayer.Entities.Resume;
using ResumeRegister.datalayer.Entities.Users;

namespace ResumeRegister.core.Services.Interfaces
{
    public interface IAccountService
    {
        #region Users

        int AddNewUser(UserRegisterViewModel user);
        void EditUser(UserEditViewModel user);
        int AdminAddNewUser(AdminUserRegisterViewModel user);
        int AdminEditUser(AdminUserEditViewModel user);
        bool IsUserNameExist(string userName);
        bool IsEmailExist(string email);
        UserInfo LoginUserInfo(LoginViewModel user);
        UserInfo GetUser(int userId);
        UserInfo GetUser(string userName);
        List<UserInfo> GetAlluser();
        bool IsUserActive(int userId);
        void DeleteUser(int userId);
        #endregion

       
        #region Role

        List<Role> GetAllRoles();
        void AddRoleToUser(List<int> roleIds, int userId);
        void DeleteRoleForUser(int userId);
        List<int>GetRolesForUser(int userId);
        void EditRolesForUser(List<int> roles, int userId);

        #endregion
    }
}

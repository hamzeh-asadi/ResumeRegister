namespace ResumeRegister.core.Services.Interfaces
{
    public interface IPermissionService
    {
        bool CheckPermission(string enPermission,string userName);

        bool IsUserAdmin(string userName);

    }
}

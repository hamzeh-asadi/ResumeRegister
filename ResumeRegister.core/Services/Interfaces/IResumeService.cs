using ResumeRegister.core.DTOs;
using ResumeRegister.datalayer.Entities.Resume;
using System.Collections.Generic;

namespace ResumeRegister.core.Services.Interfaces
{
    public interface IResumeService
    {
        #region Resume

        int AddNewResume(ResumeRegisterViewModel resume);

        void UpdateResume(ResumeEditViewModel resume);

        List<ResumeInfo> GetAllResume();

        ResumeInfo GetResumeById(int resumeId);

        ResumeDetailViewModel GetResumeDetailForShow(int resumeId);

        void UpdateResumeForAcceptedChecked(int resumeId, bool isChecked, bool isAccept);

        List<ResumeInfo> AcceptedResume();

        List<ResumeInfo> CheckedResume();

        ResumeInfo GetResumeByUserId(int userId);

        List<AdvancedSearchViewModel> AdvancedSearch(string isChecked , string isAccepted, string name , string email, string phone, string expertise,
            string majorStudy , string minorStudy, int minBackground , int maxBackground, string university , string birthLocation , string residenceLocation );

        bool IsResumeExistWithUserId(int userId);
        void DeleteResumeForUser(int userId);

        #endregion Resume
    }
}
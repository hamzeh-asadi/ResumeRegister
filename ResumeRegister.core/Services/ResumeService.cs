using ResumeRegister.core.DTOs;
using ResumeRegister.core.Services.Interfaces;
using ResumeRegister.datalayer.Context;
using ResumeRegister.datalayer.Entities.Resume;
using ResumeRegister.datalayer.Entities.Users;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ResumeRegister.core.Services
{
    public class ResumeService : IResumeService
    {
        private readonly ResumeRegisterContext _context;
        private readonly IAccountService _accountService;

        public ResumeService(ResumeRegisterContext context, IAccountService accountService)
        {
            _context = context;
            _accountService = accountService;
        }

        public int AddNewResume(ResumeRegisterViewModel resume)
        {
            ResumeInfo newResume = new ResumeInfo()
            {
                UserId = resume.UserId,
                Expertise = resume.Expertise,
                MinorStudy = resume.MinorStudy,
                MajorStudy = resume.MajorStudy,
                WorkBackground = resume.WorkBackground,
                Phone = resume.Phone,
                ResumeName = resume.ResumeName,
                RegisterDate = DateTime.Now,
                IsAccepted = false,
                IsChecked = false,
                CityOfBirth = resume.CityOfBirth,
                ProvinceOfBirth = resume.ProvinceOfBirth,
                University = resume.University,
                ProvinceOfResidence = resume.ProvinceOfResidence,
                CityOfResidence = resume.CityOfResidence,
                PictureName = resume.PictureName,
                Birthday = resume.Birthday,
            };
            _context.ResumeInfos.Add(newResume);
            _context.SaveChanges();
            return newResume.ResumeId;
        }

        public void UpdateResume(ResumeEditViewModel resume)
        {
            ResumeInfo newResume = GetResumeById(resume.ResumeId);
            newResume.Expertise = resume.Expertise;
            newResume.MinorStudy = resume.MinorStudy;
            newResume.MajorStudy = resume.MajorStudy;
            newResume.WorkBackground = resume.WorkBackground;
            newResume.Phone = resume.Phone;
            newResume.ResumeName = resume.ResumeName;
            newResume.RegisterDate = DateTime.Now;
            newResume.IsAccepted = false;
            newResume.IsChecked = false;
            newResume.Birthday = resume.Birthday;
            newResume.ProvinceOfBirth = resume.ProvinceOfBirth;
            newResume.CityOfBirth = resume.CityOfBirth;
            newResume.ProvinceOfResidence = resume.ProvinceOfResidence;
            newResume.CityOfResidence = resume.CityOfResidence;
            newResume.University = resume.University;
            newResume.PictureName = resume.PictureName;
            _context.ResumeInfos.Update(newResume);
            _context.SaveChanges();
        }

        public List<ResumeInfo> GetAllResume()
        {
            return _context.ResumeInfos.ToList();
        }

        public ResumeInfo GetResumeById(int resumeId)
        {
            return _context.ResumeInfos.Find(resumeId);
        }

        public ResumeDetailViewModel GetResumeDetailForShow(int resumeId)
        {
            var resume = GetResumeById(resumeId);
            var user = _accountService.GetUser(resume.UserId);
            ResumeDetailViewModel detail = new ResumeDetailViewModel()
            {
                UserId = resume.UserId,
                FullName = user.FullName,
                MinorStudy = resume.MinorStudy,
                Phone = resume.Phone,
                WorkBackground = resume.WorkBackground,
                MajorStudy = resume.MajorStudy,
                ResumeId = resumeId,
                Email = user.Email,
                Expertise = resume.Expertise,
                ResumeName = resume.ResumeName,
                IsAccepted = resume.IsAccepted,
                IsChecked = resume.IsChecked,
                PictureName = resume.PictureName,
                Birthday = resume.Birthday,
                CityOfBirth = resume.CityOfBirth,
                CityOfResidence = resume.CityOfResidence,
                ProvinceOfBirth = resume.ProvinceOfBirth,
                ProvinceOfResidence = resume.ProvinceOfResidence,
                University = resume.University
            };
            return detail;
        }

        public void UpdateResumeForAcceptedChecked(int resumeId, bool isChecked, bool isAccept)
        {
            var resume = GetResumeById(resumeId);
            resume.IsChecked = isChecked;
            resume.IsAccepted = isAccept;
            _context.ResumeInfos.Update(resume);
            _context.SaveChanges();
        }

        public List<ResumeInfo> AcceptedResume()
        {
            return _context.ResumeInfos.Where(r => r.IsAccepted == true).ToList();
        }

        public List<ResumeInfo> CheckedResume()
        {
            return _context.ResumeInfos.Where(r => r.IsChecked == true).ToList();
        }

        public ResumeInfo GetResumeByUserId(int userId)
        {
            return _context.ResumeInfos.FirstOrDefault(r => r.UserId == userId);
        }

        public List<AdvancedSearchViewModel> AdvancedSearch(string isChecked, string isAccepted, string name, string email,
            string phone, string expertise, string majorStudy, string minorStudy, int minBackground, int maxBackground,
            string university, string birthLocation, string residenceLocation)
        {
            List<AdvancedSearchViewModel> result = new List<AdvancedSearchViewModel>();
            IEnumerable<UserInfo> userList = _context.UserInfos;
            foreach (var user in userList)
            {
                var resume = GetResumeByUserId(user.UserId);
                if (resume != null)
                {
                    result.Add(new AdvancedSearchViewModel()
                    {
                        FullName = user.FullName,
                        IsChecked = resume.IsChecked,
                        IsAccepted = resume.IsAccepted,
                        UserId = user.UserId,
                        Email = user.Email,
                        WorkBackground = resume.WorkBackground,
                        MajorStudy = resume.MajorStudy,
                        MinorStudy = resume.MinorStudy,
                        Phone = resume.Phone,
                        Expertise = resume.Expertise,
                        ResumeId = resume.ResumeId,
                        University = resume.University,
                        BirthLocation = resume.ProvinceOfBirth+" "+resume.CityOfBirth,
                        ResidenceLocation = resume.ProvinceOfResidence+" "+resume.CityOfResidence
                    });
                }
                else
                {
                    result.Add(new AdvancedSearchViewModel()
                    {
                        FullName = user.FullName,
                        IsChecked = null,
                        IsAccepted = null,
                        UserId = user.UserId,
                        Email = user.Email,
                        WorkBackground = 0,
                        MajorStudy = null,
                        MinorStudy = null,
                        Phone = null,
                        Expertise = null,
                        ResumeId = 0,
                        University = null,
                        BirthLocation = null,
                        ResidenceLocation = null
                    });
                }
            }
            if (!string.IsNullOrEmpty(name))
            {
                result = result.Where(r => r.FullName.Contains(name)).ToList();
            }
            if (!string.IsNullOrEmpty(email))
            {
                result = result.Where(r => r.Email.Contains(email)).ToList();
            }
            if (!string.IsNullOrEmpty(phone))
            {
                result = result.Where(r => r.Phone.Contains(phone)).ToList();
            }
            if (!string.IsNullOrEmpty(expertise))
            {
                result = result.Where(r => r.Expertise.Contains(expertise)).ToList();
            }
            if (!string.IsNullOrEmpty(majorStudy))
            {
                result = result.Where(r => r.MajorStudy.Contains(majorStudy)).ToList();
            }
            if (!string.IsNullOrEmpty(minorStudy))
            {
                result = result.Where(r => r.MinorStudy.Contains(minorStudy)).ToList();
            }
            if (!string.IsNullOrEmpty(university))
            {
                result = result.Where(r => r.University.Contains(university)).ToList();
            }
            if (!string.IsNullOrEmpty(birthLocation))
            {
                result = result.Where(r => r.BirthLocation.Contains(birthLocation)).ToList();
            }
            if (!string.IsNullOrEmpty(residenceLocation))
            {
                result = result.Where(r => r.ResidenceLocation.Contains(residenceLocation)).ToList();
            }
            if (minBackground > 0)
            {
                result = result.Where(r => r.WorkBackground > minBackground).ToList();
            }
            if (maxBackground > 0)
            {
                result = result.Where(r => r.WorkBackground < maxBackground).ToList();
            }
            if (!string.IsNullOrEmpty(isChecked))
            {
                if (isChecked == "true")
                {
                    result = result.Where(r => r.IsChecked == true).ToList();
                }
                else
                {
                    result = result.Where(r => r.IsChecked == false).ToList();
                }
            }
            if (!string.IsNullOrEmpty(isAccepted))
            {
                if (isAccepted == "true")
                {
                    result = result.Where(r => r.IsAccepted == true).ToList();
                }
                else
                {
                    result = result.Where(r => r.IsAccepted == false).ToList();
                }
            }
            return result;
        }

        public bool IsResumeExistWithUserId(int userId)
        {
            return _context.ResumeInfos.Any(r => r.UserId == userId);
        }

        public void DeleteResumeForUser(int userId)
        {
            List<ResumeInfo> resumeInfos = _context.ResumeInfos.Where(r => r.UserId == userId).ToList();
            foreach (var resumeInfo in resumeInfos)
            {
                string oldFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", resumeInfo.ResumeName);
                if (System.IO.File.Exists(oldFile))
                {
                    System.IO.File.Delete(oldFile);
                }

                _context.ResumeInfos.Remove(resumeInfo);
            }
            _context.SaveChanges();
        }
    }
}
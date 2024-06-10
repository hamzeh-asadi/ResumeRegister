using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ResumeRegister.datalayer.Entities.Resume;

namespace ResumeRegister.datalayer.Entities.Users
{
    public class UserInfo
    {
        public UserInfo()
        {

        }
        [Key]
        public int UserId { get; set; }

        [Display(Name = "نام کاربری"), Required(ErrorMessage = "لطفا {0} را وارد کنید ."),
         MaxLength(100, ErrorMessage = "طول {0} نباید بیشتر از {1} کاراکتر باشد .")]
        public string UserName { get; set; }

        [Display(Name = "نام"), Required(ErrorMessage = "لطفا {0} را وارد کنید ."),
         MaxLength(100, ErrorMessage = "طول {0} نباید بیشتر از {1} کاراکتر باشد .")]
        public string FirstNameUser { get; set; }

        [Display(Name = "ایمیل"), Required(ErrorMessage = "لطفا {0} را وارد کنید ."),
         MaxLength(100, ErrorMessage = "طول {0} نباید بیشتر از {1} کاراکتر باشد ."), EmailAddress]
        public string Email { get; set; }

        [Display(Name = "نام خانوادگی"), Required(ErrorMessage = "لطفا {0} را وارد کنید ."),
         MaxLength(100, ErrorMessage = "طول {0} نباید بیشتر از {1} کاراکتر باشد .")]
        public string LastNameUser { get; set; }

        [Display(Name = "رمز عبور"), Required(ErrorMessage = "لطفا {0} را وارد کنید ."),
         MaxLength(100, ErrorMessage = "طول {0} نباید بیشتر از {1} کاراکتر باشد .")]
        public string Password { get; set; }

        [Display(Name = "تاریخ ثبت")]
        public DateTime RegisterDate { get; set; }

        [Display(Name = "فعال/غیرفعال")]
        public bool IsActive { get; set; }

        public string FullName
        {
            get { return FirstNameUser + " " + LastNameUser; }
        }

        #region Relations

        public ResumeInfo ResumeInfo { get; set; }

        #endregion
    }
}

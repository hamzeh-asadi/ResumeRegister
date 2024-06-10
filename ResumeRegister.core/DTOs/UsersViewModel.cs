using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ResumeRegister.core.DTOs
{
    public class UserRegisterViewModel
    {
        [Display(Name = "نام کاربری"), Required(ErrorMessage = "لطفا {0} را وارد کنید ."),
         MaxLength(100, ErrorMessage = "طول {0} نباید بیشتر از {1} کاراکتر باشد .")]
        public string UserName { get; set; }

        [Display(Name = "نام"), Required(ErrorMessage = "لطفا {0} را وارد کنید ."),
         MaxLength(100, ErrorMessage = "طول {0} نباید بیشتر از {1} کاراکتر باشد .")]
        public string FirstNameUser { get; set; }

        [Display(Name = "نام خانوادگی"), Required(ErrorMessage = "لطفا {0} را وارد کنید ."),
         MaxLength(100, ErrorMessage = "طول {0} نباید بیشتر از {1} کاراکتر باشد .")]
        public string LastNameUser { get; set; }

        [Display(Name = "ایمیل"), Required(ErrorMessage = "لطفا {0} را وارد کنید ."),
         MaxLength(100, ErrorMessage = "طول {0} نباید بیشتر از {1} کاراکتر باشد ."), EmailAddress]
        public string Email { get; set; }

        [Display(Name = "رمز عبور"), Required(ErrorMessage = "لطفا {0} را وارد کنید ."),
         MaxLength(100, ErrorMessage = "طول {0} نباید بیشتر از {1} کاراکتر باشد .")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "کلمه عبور و تکرار ان برابر نیستند !!!"), Display(Name = "تکرار کلمه عبور"),
         Required(ErrorMessage = "لطفا {0} را وارد کنید ."),
         MaxLength(100, ErrorMessage = "طول {0} نباید بیشتر از {1} کاراکتر باشد .")]
        public string RePassword { get; set; }
    }

    public class UserEditViewModel
    {
        public int UserId { get; set; }

        [Display(Name = "نام کاربری"), Required(ErrorMessage = "لطفا {0} را وارد کنید ."),
         MaxLength(100, ErrorMessage = "طول {0} نباید بیشتر از {1} کاراکتر باشد .")]
        public string UserName { get; set; }

        [Display(Name = "نام"), Required(ErrorMessage = "لطفا {0} را وارد کنید ."),
         MaxLength(100, ErrorMessage = "طول {0} نباید بیشتر از {1} کاراکتر باشد .")]
        public string FirstNameUser { get; set; }

        [Display(Name = "نام خانوادگی"), Required(ErrorMessage = "لطفا {0} را وارد کنید ."),
         MaxLength(100, ErrorMessage = "طول {0} نباید بیشتر از {1} کاراکتر باشد .")]
        public string LastNameUser { get; set; }

        [Display(Name = "ایمیل"), Required(ErrorMessage = "لطفا {0} را وارد کنید ."),
         MaxLength(100, ErrorMessage = "طول {0} نباید بیشتر از {1} کاراکتر باشد ."), EmailAddress]
        public string Email { get; set; }

        [Display(Name = "رمز عبور"), MaxLength(100, ErrorMessage = "طول {0} نباید بیشتر از {1} کاراکتر باشد .")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "کلمه عبور و تکرار ان برابر نیستند !!!"),
         Display(Name = "تکرار کلمه عبور"), MaxLength(100, ErrorMessage = "طول {0} نباید بیشتر از {1} کاراکتر باشد .")]
        public string RePassword { get; set; }
    }

    public class AdminUserRegisterViewModel
    {
        public bool IsActive { get; set; }
        public int RoleId { get; set; }

        [Display(Name = "نام کاربری"), Required(ErrorMessage = "لطفا {0} را وارد کنید ."),
         MaxLength(100, ErrorMessage = "طول {0} نباید بیشتر از {1} کاراکتر باشد .")]
        public string UserName { get; set; }

        [Display(Name = "نام"), Required(ErrorMessage = "لطفا {0} را وارد کنید ."),
         MaxLength(100, ErrorMessage = "طول {0} نباید بیشتر از {1} کاراکتر باشد .")]
        public string FirstNameUser { get; set; }

        [Display(Name = "نام خانوادگی"), Required(ErrorMessage = "لطفا {0} را وارد کنید ."),
         MaxLength(100, ErrorMessage = "طول {0} نباید بیشتر از {1} کاراکتر باشد .")]
        public string LastNameUser { get; set; }

        [Display(Name = "ایمیل"), Required(ErrorMessage = "لطفا {0} را وارد کنید ."),
         MaxLength(100, ErrorMessage = "طول {0} نباید بیشتر از {1} کاراکتر باشد ."), EmailAddress]
        public string Email { get; set; }

        [Display(Name = "رمز عبور"), Required(ErrorMessage = "لطفا {0} را وارد کنید ."),
         MaxLength(100, ErrorMessage = "طول {0} نباید بیشتر از {1} کاراکتر باشد .")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "کلمه عبور و تکرار ان برابر نیستند !!!"), Display(Name = "تکرار کلمه عبور"),
         Required(ErrorMessage = "لطفا {0} را وارد کنید ."),
         MaxLength(100, ErrorMessage = "طول {0} نباید بیشتر از {1} کاراکتر باشد .")]
        public string RePassword { get; set; }
    }

    public class AdminUserEditViewModel
    {
        public int UserId { get; set; }
        public bool IsActive { get; set; }
        
        public List<int> RoleIds { get; set; }

        [Display(Name = "نام کاربری"), Required(ErrorMessage = "لطفا {0} را وارد کنید ."),
         MaxLength(100, ErrorMessage = "طول {0} نباید بیشتر از {1} کاراکتر باشد .")]
        public string UserName { get; set; }

        [Display(Name = "نام"), Required(ErrorMessage = "لطفا {0} را وارد کنید ."),
         MaxLength(100, ErrorMessage = "طول {0} نباید بیشتر از {1} کاراکتر باشد .")]
        public string FirstNameUser { get; set; }

        [Display(Name = "نام خانوادگی"), Required(ErrorMessage = "لطفا {0} را وارد کنید ."),
         MaxLength(100, ErrorMessage = "طول {0} نباید بیشتر از {1} کاراکتر باشد .")]
        public string LastNameUser { get; set; }

        [Display(Name = "ایمیل"), Required(ErrorMessage = "لطفا {0} را وارد کنید ."),
         MaxLength(100, ErrorMessage = "طول {0} نباید بیشتر از {1} کاراکتر باشد ."), EmailAddress]
        public string Email { get; set; }

        [Display(Name = "رمز عبور"), MaxLength(100, ErrorMessage = "طول {0} نباید بیشتر از {1} کاراکتر باشد .")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "کلمه عبور و تکرار ان برابر نیستند !!!"),
         Display(Name = "تکرار کلمه عبور"), MaxLength(100, ErrorMessage = "طول {0} نباید بیشتر از {1} کاراکتر باشد .")]
        public string RePassword { get; set; }
    }

    public class LoginViewModel
    {
        [Display(Name = "نام کاربری یا ایمیل"), Required(ErrorMessage = "لطفا {0} را وارد کنید ."),
         MaxLength(100, ErrorMessage = "طول {0} نباید بیشتر از {1} کاراکتر باشد .")]
        public string UserName { get; set; }

        [Display(Name = "رمز عبور"), Required(ErrorMessage = "لطفا {0} را وارد کنید ."),
         MaxLength(100, ErrorMessage = "طول {0} نباید بیشتر از {1} کاراکتر باشد .")]
        public string Password { get; set; }

        [Display(Name = "مرا به خاطر بسپار ")]
        public bool RememberMe { get; set; }
    }

    public class UserFullNameAndIdViewModel
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
    }
}
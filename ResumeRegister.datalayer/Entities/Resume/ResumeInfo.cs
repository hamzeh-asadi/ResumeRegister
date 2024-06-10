using ResumeRegister.datalayer.Entities.Users;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeRegister.datalayer.Entities.Resume
{
    public class ResumeInfo
    {
        public ResumeInfo()
        {
        }

        [Key]
        public int ResumeId { get; set; }

        [Display(Name = "رشته تحصیلی"), Required(ErrorMessage = "لطفا {0} را وارد کنید"), MaxLength(100, ErrorMessage = "طول {0} نباید بیشتر از {1} کاراکتر باشد .")]
        public string MajorStudy { get; set; }

        [Display(Name = "گرایش تحصیلی"), Required(ErrorMessage = "لطفا {0} را وارد کنید"), MaxLength(100, ErrorMessage = "طول {0} نباید بیشتر از {1} کاراکتر باشد .")]
        public string MinorStudy { get; set; }

        [Display(Name = "سابقه کار(سال)"), Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int WorkBackground { get; set; }

        [Display(Name = "تلفن"), Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Phone { get; set; }

        [Display(Name = "تخصص")]
        public string Expertise { get; set; }

        public string ResumeName { get; set; }
        public DateTime RegisterDate { get; set; }

        public int UserId { get; set; }

        public bool IsAccepted { get; set; }
        public bool IsChecked { get; set; }

        #region Added Property

        [Display(Name = "سال تولد"), Required(ErrorMessage = "لطفا {0} را وارد کنید"), MaxLength(4, ErrorMessage = "طول {0} نباید بیشتر از {1} کاراکتر باشد .")]
        public string Birthday { get; set; }

        [Display(Name = "استان محل تولد"), Required(ErrorMessage = "لطفا {0} را وارد کنید"), MaxLength(100, ErrorMessage = "طول {0} نباید بیشتر از {1} کاراکتر باشد .")]
        public string ProvinceOfBirth { get; set; }

        [Display(Name = "شهر محل تولد"), Required(ErrorMessage = "لطفا {0} را وارد کنید"), MaxLength(100, ErrorMessage = "طول {0} نباید بیشتر از {1} کاراکتر باشد .")]
        public string CityOfBirth { get; set; }

        [Display(Name = "استان محل سکونت"), Required(ErrorMessage = "لطفا {0} را وارد کنید"), MaxLength(100, ErrorMessage = "طول {0} نباید بیشتر از {1} کاراکتر باشد .")]
        public string ProvinceOfResidence { get; set; }

        [Display(Name = "شهر محل سکونت"), Required(ErrorMessage = "لطفا {0} را وارد کنید"), MaxLength(100, ErrorMessage = "طول {0} نباید بیشتر از {1} کاراکتر باشد .")]
        public string CityOfResidence { get; set; }

        [Display(Name = "دانشگاه"), Required(ErrorMessage = "لطفا {0} را وارد کنید"), MaxLength(100, ErrorMessage = "طول {0} نباید بیشتر از {1} کاراکتر باشد .")]
        public string University { get; set; }

        [Display(Name = "عکس پرسنلی"), Required(ErrorMessage = "لطفا {0} را وارد کنید"), MaxLength(100, ErrorMessage = "طول {0} نباید بیشتر از {1} کاراکتر باشد .")]
        public string PictureName { get; set; }

        #endregion Added Property

        #region Relations

        [ForeignKey("UserId")]
        public UserInfo UserInfo { get; set; }

        #endregion Relations
    }
}
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ResumeRegister.core.DTOs
{
    public class AdvancedSearchViewModel
    {
        [Key] public int ResumeId { get; set; }

        [Display(Name = "رشته تحصیلی")]
        [MaxLength(100, ErrorMessage = "طول {0} نباید بیشتر از {1} کاراکتر باشد .")]
        public string MajorStudy { get; set; }

        [Display(Name = "گرایش تحصیلی")]
        [MaxLength(100, ErrorMessage = "طول {0} نباید بیشتر از {1} کاراکتر باشد .")]
        public string MinorStudy { get; set; }

        [Display(Name = "سابقه کار(سال)")]
        public int WorkBackground { get; set; }

        [Display(Name = "تلفن")]
        public string Phone { get; set; }

        [Display(Name = "تخصص")] public string Expertise { get; set; }

        public int UserId { get; set; }
        [AllowNull]
        public bool? IsAccepted { get; set; }
        [AllowNull]
        public bool? IsChecked { get; set; }

        [Display(Name = "ایمیل")]
        [MaxLength(100, ErrorMessage = "طول {0} نباید بیشتر از {1} کاراکتر باشد .")]
        public string Email { get; set; }

        public string FullName { get; set; }

        public string University { get; set; }
        public string BirthLocation { get; set; }
        public string ResidenceLocation { get; set; }
    }
}
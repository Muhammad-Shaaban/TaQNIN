using System.ComponentModel.DataAnnotations;

namespace TaQNIN1.Models
{
    public class ExternalLoginConfirmationViewModel
    {
           [Required(ErrorMessage = "اسم المستخدم مطلوب")]
        [Display(Name = "User name")]
        public string UserName { get; set; }
    }

    public class ManageUserViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "الرقم السري غير متطابق")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "اسم المستخدم مطلوب")]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required(ErrorMessage="الرقم السري مطلوب")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
        
    }

    public class RegisterViewModel
    {
         [Required(ErrorMessage = "اسم المستخدم مطلوب")]
        [Display(Name = "User name")]
        public string UserName { get; set; }

          [Required(ErrorMessage = "الرقم السري مطلوب")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "الرقم السري غير متطابق")]
        public string ConfirmPassword { get; set; }
      
    }
}

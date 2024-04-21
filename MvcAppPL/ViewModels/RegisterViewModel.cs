using System.ComponentModel.DataAnnotations;

namespace MvcAppPL.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="First Name Is Required")]
        public string Fname { get; set; }

        [Required(ErrorMessage = "Last Name Is Required")]
        public string Lname { get; set; }

        [Required(ErrorMessage = "Email Is Required")]
        [EmailAddress(ErrorMessage ="Invalid Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password Is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password Is Required")]
        [DataType(DataType.Password)]
        [Compare("Password" , ErrorMessage ="Password Dosen't Match")]
        public string ConfirmPassword { get; set; }

        public bool IsAgree { get; set; }

    }
}

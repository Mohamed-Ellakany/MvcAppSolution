using System.ComponentModel.DataAnnotations;

namespace MvcAppPL.ViewModels
{
    public class ResetPasswordViewModel
    {

        [Required(ErrorMessage ="new Password is Required")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "new Password is Required")]
        [DataType(DataType.Password)]
        [Compare("NewPassword" , ErrorMessage ="password dosen't match")]
        public string ConfirmNewPassword { get; set; }
    }
}

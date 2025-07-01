using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WILWebApp.Models
{
    public class LoginViewModel
    {
        // --- USERNAME COLUMN --- //

        [Required(ErrorMessage = "Username or Email is required!")] // SETTING REQUIRED ENTRY FOR USERNAME FIELD
        [StringLength(150, MinimumLength = 5, ErrorMessage = "MAX 150 or Min 5 Characters is required!")] // LIMIT OF username CHARACTERS
        [DisplayName("Username or Email")]
        public string username_or_email { get; set; }

        // --- PWD COLUMN --- //

        [Required(ErrorMessage = "Password is required !")] // SETTING REQUIRED ENTRY FOR PASSWORD FIELD
        [StringLength(25, MinimumLength = 8, ErrorMessage = "MAX 25 or Min 8 Characters is required!")] // LIMIT OF pwd CHARACTERS
        [DataType(DataType.Password)] //SETTING DATATYPE OF COLUMN TO PASSWORD
        public string pwd { get; set; }

    }
}

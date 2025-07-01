using System.ComponentModel.DataAnnotations;

namespace WILWebApp.Models
{
    public class RegistrationViewModel
    {
        public int Id { get; set; }

        //CREATING THE COLUMNS FOR USERNAME, PWD, AND EMAIL

        // --- USERNAME COLUMN --- //

        [Required(ErrorMessage = "Username is required!")] // SETTING REQUIRED ENTRY FOR USERNAME FIELD
        [StringLength(10, MinimumLength = 5, ErrorMessage = "MAX 10 or Min 5 Characters is required!")] // LIMIT OF username CHARACTERS
        public string username { get; set; }

        // --- PWD COLUMN --- //

        [Required(ErrorMessage = "Password is required !")] // SETTING REQUIRED ENTRY FOR PASSWORD FIELD
        [StringLength(25, MinimumLength = 8, ErrorMessage = "MAX 25 or Min 8 Characters is required!")] // LIMIT OF pwd CHARACTERS
        [DataType(DataType.Password)] //SETTING DATATYPE OF COLUMN TO PASSWORD
        public string pwd { get; set; }


        // --- EMAIL COLUMN --- //

        [Required(ErrorMessage = "Email is required!")] //SETTING REQUIRED ENTRY FOR EMAIL FIELD
        [DataType(DataType.EmailAddress)] //SETTING DATATYPE OF COLUMN TO EMAIL
        [MaxLength(150, ErrorMessage = "MAX Email Length is 150 Characters!")] // LIMIT OF EMAIL CHARACTERS

        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
            ErrorMessage = "Error, Please enter a valid Email!")]
        public string email { get; set; }
    }
}

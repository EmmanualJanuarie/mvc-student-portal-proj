using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace WILWebApp.Entities
{
    // ADDING UNIQUENESS OF Username, PWD AND EMAIL DATA//
    [Index(nameof(username), IsUnique = true)] // - SET username AS UNIQUE
    [Index(nameof(pwd), IsUnique = true)] // - SET pwd AS UNIQUE
    [Index(nameof(email), IsUnique = true)] // - SET email AS UNIQUE
    public class UserAccount
    {

        //SETTING ID COLUMN AS PRIMARY KEY
        [Key]
        public int Id { get; set; }

        //CREATING THE COLUMNS FOR USERNAME, PWD, AND EMAIL

        // --- USERNAME COLUMN --- //

        [Required(ErrorMessage = "Username is required!")] // SETTING REQUIRED ENTRY FOR USERNAME FIELD
        [MaxLength(50, ErrorMessage = "MAX Username Length is 50 Characters!")] // LIMIT OF pwd CHARACTERS
        public string username { get; set; }

        // --- PWD COLUMN --- //

        [Required(ErrorMessage = "Password is required !")] // SETTING REQUIRED ENTRY FOR PASSWORD FIELD
        [MaxLength(80, ErrorMessage = "MAX Password Length is 80 Characters!")] // LIMIT OF pwd CHARACTERS
   
        public string pwd { get; set; }

        
        // --- EMAIL COLUMN --- //

        [Required(ErrorMessage = "Email is required!")] //SETTING REQUIRED ENTRY FOR EMAIL FIELD
        [MaxLength(150, ErrorMessage = "MAX Email Length is 150 Characters!")] // LIMIT OF EMAIL CHARACTERS
        
        public string email { get; set; }
        

    }
}

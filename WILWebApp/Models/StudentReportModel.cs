using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace WILWebApp.Entities
{
    // ADDING UNIQUENESS OF Username, PWD AND EMAIL DATA//
    [Index(nameof(incidentType), IsUnique = true)] // - SET username AS UNIQUE
    [Index(nameof(description), IsUnique = true)] // - SET pwd AS UNIQUE
    [Index(nameof(location), IsUnique = true)] // - SET email AS UNIQUE
    [Index(nameof(reportedAt), IsUnique = true)] // - SET email AS UNIQUE
    [Index(nameof(status), IsUnique = true)] // - SET email AS UNIQUE
    public class StudentReport
    {

        //SETTING ID COLUMN AS PRIMARY KEY
        [Key]
        public int Id { get; set; }

        //CREATING THE COLUMNS FOR USERNAME, PWD, AND EMAIL

        // --- Incident Type  COLUMN --- //

        [Required(ErrorMessage = "Incident Type is required!")] // SETTING REQUIRED ENTRY FOR USERNAME FIELD
        [MaxLength(80, ErrorMessage = "MAX Username Length is 50 Characters!")] // LIMIT OF pwd CHARACTERS
        public string incidentType { get; set; }

        // --- Description COLUMN --- //

        [Required(ErrorMessage = "Description is required !")] // SETTING REQUIRED ENTRY FOR PASSWORD FIELD
        [MaxLength(225, ErrorMessage = "MAX Description Length is 225 Characters!")] // LIMIT OF pwd CHARACTERS

        public string description { get; set; }


        // --- Location COLUMN --- //

        [Required(ErrorMessage = "Location is required!")] //SETTING REQUIRED ENTRY FOR EMAIL FIELD
        [MaxLength(225, ErrorMessage = "MAX Location Length is 150 Characters!")] // LIMIT OF EMAIL CHARACTERS

        public string location { get; set; }

        // --- ReportAt COLUMN --- //

        [Required(ErrorMessage = "Date is required!")] //SETTING REQUIRED ENTRY FOR EMAIL FIELD

        public DateTime reportedAt { get; set; }

        // --- status COLUMN --- //

        [Required(ErrorMessage = "Status is required!")] //SETTING REQUIRED ENTRY FOR EMAIL FIELD
        [MaxLength(20, ErrorMessage = "MAX Location Length is 150 Characters!")] // LIMIT OF EMAIL CHARACTERS

        public string status { get; set; }


    }
}
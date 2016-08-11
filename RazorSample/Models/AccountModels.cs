using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Web.Security;

namespace RazorSample.Models
{
    public class RegistrationModel
    {
        [Required(ErrorMessage = "Name Required")]
        [DisplayName("Full Name:")]
        [StringLength(100, ErrorMessage = "Less than 50 characters")]
        public string Name { get; set; }

        [DisplayName("Gender:")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Date of Birth Required")]
        [DisplayName("Date Of Birth:")]
        public string DOB { get; set; }

        [Required(ErrorMessage = "Phone Number Required")]
        [DisplayName("Phone")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Email Id Required")]
        [DisplayName("Email ID")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email Format is wrong")]
        [StringLength(50, ErrorMessage = "Less than 50 characters")]
        public string Email { get; set; }

        public string Languages { get; set; }
        public string Country { get; set; }

        [Required(ErrorMessage = "Username Required")]
        [DisplayName("Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password Required")]
        [DisplayName("Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Re-enter Password")]
        [DisplayName("Confirm Password:")]
        public string ConfirmPassword { get; set; }
        public bool AgreePolicy { get; set; }
    }

    public class LoginModel
    {
        [Required(ErrorMessage = "Enter Username")]
        [DisplayName("Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Enter Password")]
        [DisplayName("Password")]
        public string Password { get; set; }
    }

    public class AccountHomeModel
    {
        public string Name { get; set; }
    }
}
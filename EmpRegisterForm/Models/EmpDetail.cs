using EmpRegisterForm.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmpRegisterForm.Models
{
    public class EmpDetail
    {
        public string Id { get; set; }
        [Required(ErrorMessage ="FirstName is Required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage ="LastName is Required")]
        public string LastName { get; set; }
        [Required(ErrorMessage ="Email is Required")]
        [RegularExpression(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-‌​]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$", ErrorMessage = "Email is not valid")]
        public string Email { get; set; }
        [Required(ErrorMessage ="Gender is Required")]
        public string Gender { get; set; }
        [Required(ErrorMessage ="Date Of Birth is Required")]
        public string DateOfBirth { get; set; }
        [Required(ErrorMessage ="Hobbies is Required")]
        public string hobbies { get; set; }

    }
}
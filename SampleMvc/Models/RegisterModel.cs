using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SampleMvc.Models
{
    public class RegisterModel
    {
        [Required]
        [Display(Name="User Name")]
        public string UserName { get; set; }
        [Required]
        [Display(Name="Password")]
        [DataType(DataType.Password)]
        public string UserPassword { get; set; }
        [Required]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("UserPassword")]
        public string ConfirmPassword { get; set; }
    }
}
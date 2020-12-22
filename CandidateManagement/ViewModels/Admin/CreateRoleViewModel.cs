using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CandidateManagement.ViewModels.Admin
{
    public class CreateRoleViewModel
    {
        [BindProperty]
        [Required]
        [Display(Name = "Role")]
        public string RoleName { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CandidateManagement.ViewModels.Admin
{
    public class EditRoleViewModel
    {
        public EditRoleViewModel()
        {
            Users = new List<string>();
        }
        [BindProperty]
        public string Id { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Role Name is required")]
        public string RoleName { get; set; }
        [BindProperty]
        public List<string> Users { get; }
    }
}

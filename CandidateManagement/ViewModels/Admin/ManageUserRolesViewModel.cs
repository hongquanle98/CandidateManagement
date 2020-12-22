using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandidateManagement.ViewModels.Admin
{
    public class ManageUserRolesViewModel
    {
        [BindProperty]
        public Role role { get; set; }
        public class Role
        {
            public string RoleId { get; set; }
            public string RoleName { get; set; }
            public bool IsSelected { get; set; }
        }
        [BindProperty]
        public List<Role> ListUserRoles { get; set; }
        [BindProperty]
        public string UserId { get; set; }
    }
}

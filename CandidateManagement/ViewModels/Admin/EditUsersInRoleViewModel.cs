using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandidateManagement.ViewModels.Admin
{
    public class EditUsersInRoleViewModel
    {
        [BindProperty]
        public User user { get; set; }
        public class User
        {
            public string UserId { get; set; }
            public string UserName { get; set; }
            public bool IsSelected { get; set; }
        }
        [BindProperty]
        public List<User> ListUserInRole { get; set; }
        [BindProperty]
        public string RoleId { get; set; }
    }
}

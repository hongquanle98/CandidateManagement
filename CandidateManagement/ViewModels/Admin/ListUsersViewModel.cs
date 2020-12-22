using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandidateManagement.ViewModels.Admin
{
    public class ListUsersViewModel
    {
        [BindProperty]
        public IEnumerable<IdentityUser> ListUsers { get; set; }
    }
}
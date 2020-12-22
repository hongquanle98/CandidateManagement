using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandidateManagement.ViewModels.Admin
{
    public class ManageUserClaimsViewModel
    {
        [BindProperty]
        public string UserId { get; set; }
        [BindProperty]
        public List<UserClaim> ListCliams { get; set; }
        public class UserClaim
        {
            public string ClaimType { get; set; }
            public bool IsSelected { get; set; }
        }
    }
}

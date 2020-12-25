using CandidateManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandidateManagement.ViewModels.Home
{
    public class CandidateViewModel
    {
        [BindProperty]
        public Candidate Candidate { get; set; }        
        [BindProperty]
        public IFormFile Avatar { get; set; }
        [BindProperty]
        public string UserId { get; set; }
    }
}

using CandidateManagement.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandidateManagement.ViewModels.Home
{
    public class RequirementDetailViewModel
    {
        [BindProperty]
        public Candidate Candidate { get; set; }
        [BindProperty]
        public Requirement Requirement { get; set; }
        [BindProperty]
        public IEnumerable<Requirement> OtherRequirement { get; set; }
    }
}

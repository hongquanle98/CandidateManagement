using CandidateManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandidateManagement.ViewModels.Home
{
    public class CandidateRequirementViewModel
    {
        [BindProperty]
        public Candidate Candidate { get; set; }
        [BindProperty]
        public IEnumerable<Requirement> SavedRequirements { get; set; }
        [BindProperty]
        public IEnumerable<Requirement> AppliedRequirements { get; set; }
    }
}

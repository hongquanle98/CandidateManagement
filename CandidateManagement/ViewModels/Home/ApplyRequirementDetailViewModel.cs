using CandidateManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandidateManagement.ViewModels.Home
{
    public class ApplyRequirementDetailViewModel
    {
        [BindProperty]
        public Requirement Requirement { get; set; }
        [BindProperty]
        public Candidate Candidate { get; set; }
        [BindProperty]
        public ApplyDetail ApplyDetail { get; set; }
        [BindProperty]
        public IFormFile CVFile { get; set; }
        public class Ability
        {
            public int AbilityID { get; set; }
            public string AbilityName { get; set; }
            public int Point { get; set; }
            public bool IsSelected { get; set; }
        }
        [BindProperty]
        public List<Ability> RequiredAbilities { get; set; }
        [BindProperty]
        public List<Ability> AdvantageAbilities { get; set; }
    }
}

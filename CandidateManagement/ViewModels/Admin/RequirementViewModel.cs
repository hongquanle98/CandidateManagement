using CandidateManagement.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandidateManagement.ViewModels.Admin
{
    public class RequirementViewModel
    {
        [BindProperty]
        public Requirement Requirement { get; set; }
        public class Ability
        {
            public int AbilityID { get; set; }
            public string AbilityName { get; set; }
            public bool IsSelected { get; set; }
        }
        [BindProperty]
        public List<Ability> ApplyPositionAbilities { get; set; }
        [BindProperty]
        public int SelectedSalaryAbilitiyId { set; get; }
        [BindProperty]
        public List<Models.Ability> SalaryAbilities { get; set; }
        [BindProperty]
        public int SelectedWorkedTimeAbilitiyId { set; get; }
        [BindProperty]
        public List<Models.Ability> WorkedTimeAbilities { get; set; }
    }
}

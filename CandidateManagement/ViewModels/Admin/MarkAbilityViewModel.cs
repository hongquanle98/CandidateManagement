using CandidateManagement.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CandidateManagement.ViewModels.Admin
{
    public class MarkAbilityViewModel
    {
        [BindProperty]
        public IEnumerable<InterviewResult> InterviewResult { get; set; }
        [BindProperty]
        public ApplyDetail ApplyDetail { get; set; }
        [BindProperty]
        public InterviewSchedule InterviewSchedule { get; set; }
        [BindProperty]
        public Operator Operator { get; set; }

        public class ApplyDetailAbility
        {
            public int ApplyDetailAbilityID { get; set; }
            public string AbilityName { get; set; }
            public int Point { get; set; }
            public bool IsSelected { get; set; }
        }
        [BindProperty]
        public List<ApplyDetailAbility> ApplyDetailAbilities { get; set; }
    }
}

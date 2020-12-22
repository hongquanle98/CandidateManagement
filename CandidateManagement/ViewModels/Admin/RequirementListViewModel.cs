using CandidateManagement.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandidateManagement.ViewModels.Admin
{
    public class RequirementListViewModel
    {
        [BindProperty]
        public DateTime? FromDate { get; set; }
        [BindProperty]
        public DateTime? ToDate { get; set; }
        [BindProperty]
        public string RecruitPosition { get; set; }
        [BindProperty]
        public IEnumerable<Requirement> Requirements { get; set; }
    }
}

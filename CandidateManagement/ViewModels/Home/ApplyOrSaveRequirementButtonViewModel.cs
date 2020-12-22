using CandidateManagement.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandidateManagement.ViewModels.Home
{
    public class ApplyOrSaveRequirementButtonViewModel
    {
        public Candidate Candidate { get; set; }
        public Requirement Requirement { get; set; }
        public bool IsRequirementDetail { get; set; }
    }
}

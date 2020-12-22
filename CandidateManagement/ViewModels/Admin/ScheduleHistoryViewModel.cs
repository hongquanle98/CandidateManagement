using CandidateManagement.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandidateManagement.ViewModels.Admin
{
    public class ScheduleHistoryViewModel
    {
        [BindProperty]
        public Candidate Candidate { get; set; }
        [BindProperty]
        public ApplyDetail ApplyDetail { get; set; }
        [BindProperty]
        public IEnumerable<InterviewSchedule> InterviewSchedules { get; set; }
    }
}

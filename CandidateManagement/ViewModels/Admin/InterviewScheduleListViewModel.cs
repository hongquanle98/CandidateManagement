using CandidateManagement.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandidateManagement.ViewModels.Admin
{
    public class InterviewScheduleListViewModel
    {
        [BindProperty]
        public DateTime? FromDate { get; set; }
        [BindProperty]
        public DateTime? ToDate { get; set; }
        [BindProperty]
        public string Status { get; set; }
        [BindProperty]
        public IEnumerable<InterviewSchedule> InterviewSchedules { get; set; }
    }
}

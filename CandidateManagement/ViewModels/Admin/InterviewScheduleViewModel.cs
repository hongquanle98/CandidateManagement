using CandidateManagement.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandidateManagement.ViewModels.Admin
{
    public class InterviewScheduleViewModel
    {
        [BindProperty]
        public Candidate Candidate { get; set; }
        [BindProperty]
        public ApplyDetail ApplyDetail { get; set; }
        public class Operator
        {
            public int OperatorID { get; set; }
            public string OperatorName { get; set; }
            public bool IsSelected { get; set; }
            public bool HasEvaluated { get; set; }
        }
        [BindProperty]
        public List<Operator> Operators { get; set; }
        [BindProperty]
        public InterviewSchedule InterviewSchedule { get; set; }
        [BindProperty]
        public string MailSubject { get; set; }
        [BindProperty]
        public string MailBody { get; set; }
        public List<EmailTemplate> EmailTemplates { get; set; }
        [BindProperty]
        public EmailTemplate EmailTemplate { get; set; }
    }
}

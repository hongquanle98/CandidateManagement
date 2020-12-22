using System;
using System.Collections.Generic;

namespace CandidateManagement.Models
{
    public partial class ApplyDetail
    {
        public ApplyDetail()
        {
            ApplyDetailAbility = new HashSet<ApplyDetailAbility>();
            InterviewSchedule = new HashSet<InterviewSchedule>();
        }

        public int ApplyDetailId { get; set; }
        public int ApplyId { get; set; }
        public int RequirementId { get; set; }
        public DateTime ApplyDate { get; set; }
        public decimal ExpectedSalary { get; set; }
        public int WorkedTime { get; set; }
        public bool? IsCvpass { get; set; }
        public bool? IsInterviewPass { get; set; }
        public bool? IsApplyDetailPass { get; set; }
        public string CvfilePath { get; set; }

        public Apply Apply { get; set; }
        public Requirement Requirement { get; set; }
        public ICollection<ApplyDetailAbility> ApplyDetailAbility { get; set; }
        public ICollection<InterviewSchedule> InterviewSchedule { get; set; }
    }
}

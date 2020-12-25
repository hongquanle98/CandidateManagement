using System;
using System.Collections.Generic;

namespace CandidateManagement.Models
{
    public partial class InterviewSchedule
    {
        public InterviewSchedule()
        {
            InterviewResult = new HashSet<InterviewResult>();
        }

        public int InterviewId { get; set; }
        public int ApplyDetailId { get; set; }
        public DateTime InterviewDate { get; set; }
        public TimeSpan InterviewTime { get; set; }
        public bool IsEmailSent { get; set; }
        public bool? IsInterviewPass { get; set; }

        public ApplyDetail ApplyDetail { get; set; }
        public ICollection<InterviewResult> InterviewResult { get; set; }
    }
}

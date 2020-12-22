using System;
using System.Collections.Generic;

namespace CandidateManagement.Models
{
    public partial class InterviewResult
    {
        public int InterviewId { get; set; }
        public int OperatorId { get; set; }
        public int ApplyDetailAbilityId { get; set; }
        public int Point { get; set; }

        public ApplyDetailAbility ApplyDetailAbility { get; set; }
        public InterviewSchedule Interview { get; set; }
        public Operator Operator { get; set; }
    }
}

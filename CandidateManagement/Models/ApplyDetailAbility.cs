using System;
using System.Collections.Generic;

namespace CandidateManagement.Models
{
    public partial class ApplyDetailAbility
    {
        public ApplyDetailAbility()
        {
            InterviewResult = new HashSet<InterviewResult>();
        }

        public int ApplyDetailAbilityId { get; set; }
        public int ApplyDetailId { get; set; }
        public int AbilityId { get; set; }
        public int Point { get; set; }

        public Ability Ability { get; set; }
        public ApplyDetail ApplyDetail { get; set; }
        public ICollection<InterviewResult> InterviewResult { get; set; }
    }
}

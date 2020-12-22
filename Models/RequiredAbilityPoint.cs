using System;
using System.Collections.Generic;

namespace CandidateManagement.Models
{
    public partial class RequiredAbilityPoint
    {
        public int RequiredAbilityPointId { get; set; }
        public int ApplyDetailAbilityId { get; set; }
        public int RequiredAbilityId { get; set; }
        public int Point { get; set; }

        public ApplyDetailAbility ApplyDetailAbility { get; set; }
        public RequiredAbility RequiredAbility { get; set; }
        public InterviewResult InterviewResult { get; set; }
    }
}

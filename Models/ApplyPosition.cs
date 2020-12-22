using System;
using System.Collections.Generic;

namespace CandidateManagement.Models
{
    public partial class ApplyPosition
    {
        public ApplyPosition()
        {
            ApplyPositionAbility = new HashSet<ApplyPositionAbility>();
            Requirement = new HashSet<Requirement>();
        }

        public int ApplyPositionId { get; set; }
        public string ApplyPositionName { get; set; }

        public ICollection<ApplyPositionAbility> ApplyPositionAbility { get; set; }
        public ICollection<Requirement> Requirement { get; set; }
    }
}

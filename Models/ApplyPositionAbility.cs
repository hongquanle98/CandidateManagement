using System;
using System.Collections.Generic;

namespace CandidateManagement.Models
{
    public partial class ApplyPositionAbility
    {
        public int ApplyPositionId { get; set; }
        public int AbilityId { get; set; }

        public Ability Ability { get; set; }
        public ApplyPosition ApplyPosition { get; set; }
    }
}

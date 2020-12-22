using System;
using System.Collections.Generic;

namespace CandidateManagement.Models
{
    public partial class Ability
    {
        public Ability()
        {
            ApplyDetailAbility = new HashSet<ApplyDetailAbility>();
            ApplyPositionAbility = new HashSet<ApplyPositionAbility>();
            RequiredAbility = new HashSet<RequiredAbility>();
        }

        public int AbilityId { get; set; }
        public string AbilityName { get; set; }
        public string Note { get; set; }

        public ICollection<ApplyDetailAbility> ApplyDetailAbility { get; set; }
        public ICollection<ApplyPositionAbility> ApplyPositionAbility { get; set; }
        public ICollection<RequiredAbility> RequiredAbility { get; set; }
    }
}

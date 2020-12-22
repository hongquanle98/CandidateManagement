﻿using System;
using System.Collections.Generic;

namespace CandidateManagement.Models
{
    public partial class RequiredAbility
    {
        public RequiredAbility()
        {
            RequiredAbilityPoint = new HashSet<RequiredAbilityPoint>();
        }

        public int RequiredAbilityId { get; set; }
        public int RequirementId { get; set; }
        public int AbilityId { get; set; }

        public Ability Ability { get; set; }
        public Requirement Requirement { get; set; }
        public ICollection<RequiredAbilityPoint> RequiredAbilityPoint { get; set; }
    }
}

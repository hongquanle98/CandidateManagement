using System;
using System.Collections.Generic;

namespace CandidateManagement.Models
{
    public partial class SavedRequirement
    {
        public int CandidateId { get; set; }
        public int RequirementId { get; set; }

        public Candidate Candidate { get; set; }
        public Requirement Requirement { get; set; }
    }
}

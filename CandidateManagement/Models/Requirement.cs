using System;
using System.Collections.Generic;

namespace CandidateManagement.Models
{
    public partial class Requirement
    {
        public Requirement()
        {
            ApplyDetail = new HashSet<ApplyDetail>();
            RequiredAbility = new HashSet<RequiredAbility>();
            RequirementCertificate = new HashSet<RequirementCertificate>();
            SavedRequirement = new HashSet<SavedRequirement>();
        }

        public int RequirementId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string RequireDescription { get; set; }
        public string WorkPlace { get; set; }
        public string Language { get; set; }
        public DateTime RecruitFrom { get; set; }
        public DateTime RecruitTo { get; set; }
        public int ApplyPositionId { get; set; }
        public int RecruitAmount { get; set; }
        public int ViewCount { get; set; }
        public DateTime CreateTime { get; set; }

        public ApplyPosition ApplyPosition { get; set; }
        public ICollection<ApplyDetail> ApplyDetail { get; set; }
        public ICollection<RequiredAbility> RequiredAbility { get; set; }
        public ICollection<RequirementCertificate> RequirementCertificate { get; set; }
        public ICollection<SavedRequirement> SavedRequirement { get; set; }
    }
}

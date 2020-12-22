using System;
using System.Collections.Generic;

namespace CandidateManagement.Models
{
    public partial class Candidate
    {
        public Candidate()
        {
            Apply = new HashSet<Apply>();
            SavedRequirement = new HashSet<SavedRequirement>();
        }

        public int CandidateId { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string IdentityNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PlaceOfBirth { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string AvatarPath { get; set; }
        public string UserId { get; set; }

        public CandidateBackground CandidateBackground { get; set; }
        public ICollection<Apply> Apply { get; set; }
        public ICollection<SavedRequirement> SavedRequirement { get; set; }
    }
}

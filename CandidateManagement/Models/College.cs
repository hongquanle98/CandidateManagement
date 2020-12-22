using System;
using System.Collections.Generic;

namespace CandidateManagement.Models
{
    public partial class College
    {
        public College()
        {
            CandidateBackground = new HashSet<CandidateBackground>();
        }

        public int CollegeId { get; set; }
        public string CollegeName { get; set; }

        public ICollection<CandidateBackground> CandidateBackground { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace CandidateManagement.Models
{
    public partial class Apply
    {
        public Apply()
        {
            ApplyDetail = new HashSet<ApplyDetail>();
        }

        public int ApplyId { get; set; }
        public int CandidateId { get; set; }

        public Candidate Candidate { get; set; }
        public ICollection<ApplyDetail> ApplyDetail { get; set; }
    }
}

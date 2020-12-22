using System;
using System.Collections.Generic;

namespace CandidateManagement.Models
{
    public partial class Operator
    {
        public Operator()
        {
            InterviewResult = new HashSet<InterviewResult>();
        }

        public int OperatorId { get; set; }
        public string OperatorName { get; set; }
        public string UserId { get; set; }

        public ICollection<InterviewResult> InterviewResult { get; set; }
    }
}

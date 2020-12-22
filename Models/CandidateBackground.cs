using System;
using System.Collections.Generic;

namespace CandidateManagement.Models
{
    public partial class CandidateBackground
    {
        public CandidateBackground()
        {
            CandidateCertificate = new HashSet<CandidateCertificate>();
        }

        public int CandidateId { get; set; }
        public int? CollegeId { get; set; }
        public int? CurrentCollegeYear { get; set; }
        public string Major { get; set; }
        public double? CurrentGpa { get; set; }
        public DateTime? GraduateDate { get; set; }

        public Candidate Candidate { get; set; }
        public College College { get; set; }
        public ICollection<CandidateCertificate> CandidateCertificate { get; set; }
    }
}

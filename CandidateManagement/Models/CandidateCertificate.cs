using System;
using System.Collections.Generic;

namespace CandidateManagement.Models
{
    public partial class CandidateCertificate
    {
        public int CandidateId { get; set; }
        public int CertificateId { get; set; }

        public CandidateBackground Candidate { get; set; }
        public Certificate Certificate { get; set; }
    }
}

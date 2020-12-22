using System;
using System.Collections.Generic;

namespace CandidateManagement.Models
{
    public partial class RequirementCertificate
    {
        public int RequirementId { get; set; }
        public int CertificateId { get; set; }

        public Certificate Certificate { get; set; }
        public Requirement Requirement { get; set; }
    }
}

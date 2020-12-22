using System;
using System.Collections.Generic;

namespace CandidateManagement.Models
{
    public partial class Certificate
    {
        public Certificate()
        {
            CandidateCertificate = new HashSet<CandidateCertificate>();
            RequirementCertificate = new HashSet<RequirementCertificate>();
        }

        public int CertificateId { get; set; }
        public string CertificateName { get; set; }
        public DateTime CertificateValidFrom { get; set; }
        public DateTime CertificateValidTo { get; set; }

        public ICollection<CandidateCertificate> CandidateCertificate { get; set; }
        public ICollection<RequirementCertificate> RequirementCertificate { get; set; }
    }
}

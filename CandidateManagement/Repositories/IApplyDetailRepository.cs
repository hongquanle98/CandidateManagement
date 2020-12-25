using CandidateManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandidateManagement.Repositories
{
    public interface IApplyDetailRepository
    {
        void Add(ApplyDetail applyDetail);
        void Update(ApplyDetail applyDetail);
        void Delete(ApplyDetail applyDetail);
        ApplyDetail GetApplyDetail(int applyDetailID);
        IEnumerable<ApplyDetail> GetApplyDetailsByCandidateId(int candidateID);
        IEnumerable<ApplyDetail> GetApplyDetails();
        ApplyDetail GetNewestApplyDetail();
        IEnumerable<ApplyDetail> GetApplyDetailsByRequirementId(int requirementID);
        void UpdateCVStatus(int applyDetailID, string status);
        string GetApplyDetailStatus(int applyDetailID);
    }
}

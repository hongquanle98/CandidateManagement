using CandidateManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandidateManagement.Repositories
{
    public class MockApplyDetailRepository : IApplyDetailRepository
    {
        CandidateManagementContext _context = new CandidateManagementContext();

        public void Add(ApplyDetail applyDetail)
        {
            _context.ApplyDetail.Add(applyDetail);
            _context.SaveChanges();
        }

        public void Update(ApplyDetail applyDetail)
        {
            _context.ApplyDetail.Update(applyDetail);
            _context.SaveChanges();
        }
        public void Delete(ApplyDetail applyDetail)
        {
            _context.ApplyDetail.Remove(applyDetail);
            _context.SaveChanges();
        }
        public ApplyDetail GetApplyDetail(int applyDetailID)
        {
            return _context.ApplyDetail
                .Include(ad => ad.Requirement)
                .ThenInclude(r => r.ApplyPosition)
                .Include(ad => ad.ApplyDetailAbility)
                .Include(ad => ad.InterviewSchedule)
                .Include(ad => ad.Apply)
                .FirstOrDefault(ad => ad.ApplyDetailId == applyDetailID);
        }

        public ApplyDetail GetNewestApplyDetail()
        {
            return _context.ApplyDetail.OrderBy(ad => ad.ApplyDetailId).LastOrDefault();
        }

        public IEnumerable<ApplyDetail> GetApplyDetailsByCandidateId(int candidateID)
        {
            int applyID = _context.Apply.FirstOrDefault(a => a.CandidateId == candidateID).ApplyId;
            var apllyDetails = _context.ApplyDetail
                .Include(ad => ad.Requirement)
                .ThenInclude(r => r.ApplyPosition)
                .Include(ad => ad.ApplyDetailAbility)
                .Include(ad => ad.InterviewSchedule)
                .Include(ad => ad.Apply)
                .ThenInclude(a => a.Candidate)
                .Where(ad => ad.ApplyId == applyID);
            return apllyDetails;
        }

        public IEnumerable<ApplyDetail> GetApplyDetailsByRequirementId(int requirementID)
        {
            return _context.ApplyDetail.Where(ad => ad.RequirementId == requirementID);
        }
        public void UpdateCVStatus(int applyDetailID, string status)
        {
            ApplyDetail applyDetail = GetApplyDetail(applyDetailID);
            if (applyDetail != null)
            {
                if (status.Equals("pass"))
                    applyDetail.IsCvpass = true;
                else if (status.Equals("fail"))
                    applyDetail.IsCvpass = false;
                else
                    applyDetail.IsCvpass = null;

                _context.SaveChanges();
            }
        }
        public void UpdateInterviewStatus(int applyDetailID, string status)
        {
            ApplyDetail applyDetail = GetApplyDetail(applyDetailID);
            if (applyDetail != null)
            {
                if (status.Equals("pass"))
                    applyDetail.IsInterviewPass = true;
                else if (status.Equals("fail"))
                    applyDetail.IsInterviewPass = false;
                else
                    applyDetail.IsInterviewPass = null;

                _context.SaveChanges();
            }
        }

        public string GetApplyDetailStatus(int applyDetailID)
        {
            var applyDetail = _context.ApplyDetail.FirstOrDefault(ad => ad.ApplyDetailId == applyDetailID);

            bool isCVPending = !applyDetail.IsCvpass.HasValue;
            bool isCVPass = !isCVPending && applyDetail.IsCvpass.Value;
            bool isInterviewPending = !applyDetail.IsInterviewPass.HasValue;
            bool isInterviewPass = !isInterviewPending && applyDetail.IsInterviewPass.Value;

            if (isCVPending)
                return "Chờ duyệt[text-warning]";
            if(!isCVPass)
                return "CV không phù hợp[text-danger]";
            else if (isInterviewPending)
                return "CV phù hợp[text-success]";
            if (!isInterviewPass)
                return "Kết quả phỏng vấn không đạt yêu cầu[text-danger]";
            return "Kết quả phỏng vấn đạt yêu cầu, chúc mừng bạn đã trở thành nhân viên của công ty[text-success]";
        }

        public IEnumerable<ApplyDetail> GetApplyDetails()
        {
            return _context.ApplyDetail
                .Include(ad => ad.Requirement)
                .Include(ad => ad.InterviewSchedule)
                .Include(ad => ad.Apply)
                .ThenInclude(a => a.Candidate);
        }
    }
}

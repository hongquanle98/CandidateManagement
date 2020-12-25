using CandidateManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandidateManagement.Repositories
{
    public class MockInterviewScheduleRepository : IInterviewScheduleRepository
    {
        CandidateManagementContext _context = new CandidateManagementContext();

        public void Add(InterviewSchedule interviewSchedule)
        {
            _context.InterviewSchedule.Add(interviewSchedule);
            _context.SaveChanges();
        }

        public void Update(InterviewSchedule interviewSchedule)
        {
            _context.InterviewSchedule.Update(interviewSchedule);
            _context.SaveChanges();
        }

        public void Delete(InterviewSchedule interviewSchedule)
        {
            _context.InterviewSchedule.Remove(interviewSchedule);
            _context.SaveChanges();
        }

        public void UpdateInterviewStatus(int interviewID, string status)
        {
            InterviewSchedule interviewSchedule = GetInterviewSchedule(interviewID);
            if (interviewSchedule != null)
            {
                if (status.Equals("pass"))
                    interviewSchedule.IsInterviewPass = true;
                else if (status.Equals("fail"))
                    interviewSchedule.IsInterviewPass = false;
                else
                    interviewSchedule.IsInterviewPass = null;

                _context.SaveChanges();
            }
        }

        public InterviewSchedule GetInterviewSchedule(int interviewScheduleId)
        {
            return _context.InterviewSchedule.FirstOrDefault(ise => ise.InterviewId == interviewScheduleId);
        }

        public IEnumerable<InterviewSchedule> GetInterviewScheduleByApplyDetailId(int applyDetailID)
        {
            return _context.InterviewSchedule
                .Include(ts => ts.ApplyDetail)
                .Include(ts => ts.InterviewResult)
                .ThenInclude(ts => ts.Operator)
                .Where(ts => ts.ApplyDetailId == applyDetailID);
        }

        public InterviewSchedule GetNewestInterviewSchedule()
        {
            return _context.InterviewSchedule.OrderBy(ise => ise.InterviewId).LastOrDefault();
        }

        public IEnumerable<InterviewSchedule> GetInterviewSchedules()
        {
            return _context.InterviewSchedule
                .Include(ise => ise.ApplyDetail)
                .ThenInclude(ad => ad.Apply)
                .ThenInclude(a => a.Candidate)
                .Include(ise => ise.InterviewResult);
        }
    }
}

using CandidateManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandidateManagement.Repositories
{
    public class MockInterviewResultRepository : IInterviewResultRepository
    { 
        CandidateManagementContext _context = new CandidateManagementContext();

        public void Add(InterviewResult ir)
        {
            _context.InterviewResult.Add(ir);
            _context.SaveChanges();
        }
        public void Update(InterviewResult ir)
        {
            _context.InterviewResult.Update(ir);
            _context.SaveChanges();
        }
        public void Delete(InterviewResult ir)
        {
            _context.InterviewResult.Remove(ir);
            _context.SaveChanges();
        }

        public IEnumerable<InterviewResult> GetInterviewResultByInterviewID(int interviewID)
        {
            return _context.InterviewResult
                .Include(ir => ir.Operator)
                .Where(tr => tr.InterviewId == interviewID);
        }

        public IEnumerable<InterviewResult> GetInterviewResultByInterviewIDAndOperatorID(int interviewID, int operatorID)
        {
            return _context.InterviewResult
                .Where(ir => ir.InterviewId == interviewID && ir.OperatorId == operatorID);
        }
    }
}

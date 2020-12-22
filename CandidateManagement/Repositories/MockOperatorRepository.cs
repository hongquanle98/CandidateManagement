using Microsoft.EntityFrameworkCore;
using CandidateManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandidateManagement.Repositories
{
    public class MockOperatorRepository : IOperatorRepository
    {
        CandidateManagementContext _context = new CandidateManagementContext();

        public void Add(Operator op)
        {
            _context.Operator.Update(op);
            _context.SaveChanges();
        }
        public Operator GetOperator(int operatorID)
        {
            return _context.Operator
                .Include(o => o.InterviewResult)
                .ThenInclude(iso => iso.Interview)
                .FirstOrDefault(o => o.OperatorId == operatorID);
        }

        public Operator GetOperator(string userID)
        {
            return _context.Operator
                .Include(o => o.InterviewResult)
                .FirstOrDefault(c => c.UserId == userID);
        }

        public List<Operator> GetOperators()
        {
            return _context.Operator.ToList();
        }
    }
}

using CandidateManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandidateManagement.Repositories
{
    public class MockApplyRepository : IApplyRepository
    {
        CandidateManagementContext _context = new CandidateManagementContext();

        public void Add(Apply apply)
        {
            _context.Apply.Update(apply);
            _context.SaveChanges();
        }
        public Apply GetApplyByCandidateId(int candidateID)
        {
            return _context.Apply.FirstOrDefault(a => a.CandidateId == candidateID);
        }
    }
}

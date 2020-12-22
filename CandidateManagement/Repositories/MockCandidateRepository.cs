using CandidateManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandidateManagement.Repositories
{
    public class MockCandidateRepository : ICandidateRepository
    {
        CandidateManagementContext _context = new CandidateManagementContext();
        public void Add(Candidate candidate)
        {
            _context.Candidate.Update(candidate);
            _context.SaveChanges();
        }
        public void Update(Candidate candidate)
        {
            _context.Candidate.Update(candidate);
            _context.SaveChanges();            
        }
        public Candidate GetNewestCandidate()
        {
            return _context.Candidate.OrderBy(c => c.CandidateId).LastOrDefault();
        }
        public IEnumerable<Candidate> GetCandidates()
        {
            return _context.Candidate
                .Include(c => c.CandidateBackground)
                .Include(c => c.CandidateBackground.College)
                //.Include(c => c.ApplyDetail)
                //.Include(c => c.ApplyDetail.ApplyPosition)
                //.Include(c => c.ApplyDetail.JobSource)
                //.Include(c => c.ApplyDetail.ApplyProgrammingLanguage)
                //.Include(c => c.CandidateProgrammingLanguage)
                //.ThenInclude(cpl => cpl.ProgrammingLanguage)
                ;
        }                                               
        public Candidate GetCandidate(int candidateID)
        {
            var candidate = _context.Candidate
                .Include(c => c.CandidateBackground)
                .Include(c => c.CandidateBackground.College)
                //.Include(c => c.ApplyDetail)
                //.Include(c => c.ApplyDetail.ApplyPosition)
 
                .FirstOrDefault(c => c.CandidateId == candidateID);
            return candidate;
        }
        public Candidate GetCandidate(string userID)
        {
            return _context.Candidate
                .Include(c => c.CandidateBackground)
                .Include(c => c.CandidateBackground.College)
                //.Include(c => c.ApplyDetail)
                //.Include(c => c.ApplyDetail.ApplyPosition)
                //.Include(c => c.ApplyDetail.JobSource)
                //.Include(c => c.ApplyDetail.ApplyProgrammingLanguage)
                //.Include(c => c.CandidateProgrammingLanguage)
                //.ThenInclude(cpl => cpl.ProgrammingLanguage)
                .FirstOrDefault(c => c.UserId == userID);
        }                                
    }
}


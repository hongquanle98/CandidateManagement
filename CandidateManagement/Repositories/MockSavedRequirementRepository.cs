using CandidateManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandidateManagement.Repositories
{
    public class MockSavedRequirementRepository : ISavedRequirementRepository
    {
        CandidateManagementContext _context = new CandidateManagementContext();

        public void Add(SavedRequirement savedRequirement)
        {
            _context.SavedRequirement.Add(savedRequirement);
            _context.SaveChanges();
        }
        public void Update(SavedRequirement savedRequirement)
        {
            _context.SavedRequirement.Update(savedRequirement);
            _context.SaveChanges();
        }
        public void Delete(SavedRequirement savedRequirement)
        {
            _context.SavedRequirement.Remove(savedRequirement);
            _context.SaveChanges();
        }

        public SavedRequirement GetSavedRequirement(int candidateID, int requirementID)
        {
            return _context.SavedRequirement.FirstOrDefault(sr => sr.CandidateId == candidateID && sr.RequirementId == requirementID);
        }

        public IEnumerable<SavedRequirement> GetSavedRequirements(int candidateID)
        {
            return _context.SavedRequirement
                .Include(sr => sr.Requirement)
                .Include(sr => sr.Candidate)
                .Where(sr => sr.CandidateId == candidateID);
        }
    }
}

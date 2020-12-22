using CandidateManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CandidateManagement.Repositories
{
    public class MockRequirementRepository : IRequirementRepository
    {
        CandidateManagementContext _context = new CandidateManagementContext();


        public void Add(Requirement requirement)
        {
            _context.Requirement.Add(requirement);
            _context.SaveChanges();
        }
        public void Update(Requirement requirement)
        {
            _context.Requirement.Update(requirement);
            _context.SaveChanges();
        }
        public void Delete(Requirement requirement)
        {
            _context.Requirement.Remove(requirement);
            _context.SaveChanges();
        }
        public IEnumerable<Requirement> GetOtherRequirement(int requirementID)
        {
            return _context.Requirement
                .Include(r => r.ApplyPosition)
                .Where(r => r.RequirementId != requirementID)
                .OrderByDescending(r => r.CreateTime);
        }

        public Requirement GetRequirement(int requirementID)
        {
            return _context.Requirement
                .Include(r => r.ApplyPosition)
                .FirstOrDefault(r => r.RequirementId == requirementID);
        }

        public IEnumerable<Requirement> GetRequirements()
        {
            return _context.Requirement
                .Include(r => r.ApplyPosition)
                .OrderBy(r => r.RequirementId);
        }

        public Requirement GetNewestRequirement()
        {
            return _context.Requirement.OrderBy(r => r.RequirementId).LastOrDefault();
        }
    }
}

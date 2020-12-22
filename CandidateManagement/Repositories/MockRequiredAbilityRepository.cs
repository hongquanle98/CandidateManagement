using CandidateManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandidateManagement.Repositories
{
    public class MockRequiredAbilityRepository : IRequiredAbilityRepository
    {
        CandidateManagementContext _context = new CandidateManagementContext();

        public void Add(RequiredAbility requiredAbility)
        {
            _context.RequiredAbility.Add(requiredAbility);
            _context.SaveChanges();
        }

        public void Delete(RequiredAbility requiredAbility)
        {
            _context.RequiredAbility.Remove(requiredAbility);
            //_context.SaveChanges();
        }
        public IEnumerable<RequiredAbility> GetRequiredAbilities(int requirementID)
        {
            return _context.RequiredAbility
                .Include(ra => ra.Requirement)
                .Include(ra => ra.Ability)
                .Where(ra => ra.RequirementId == requirementID);
        }

        public RequiredAbility GetRequiredAbility(int requiredAbilityId)
        {
            return _context.RequiredAbility
                .Include(ra => ra.Requirement)
                .Include(ra => ra.Ability)
                .FirstOrDefault(ra => ra.RequiredAbilityId == requiredAbilityId);
        }

    }
}

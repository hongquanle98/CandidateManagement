using CandidateManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandidateManagement.Repositories
{
    public class MockApplyPositionAbilityRepository : IApplyPositionAbilityRepository
    {
        CandidateManagementContext _context = new CandidateManagementContext();

        public IEnumerable<ApplyPositionAbility> GetApplyPositionAbilities(int applyPositionId)
        {
            return _context.ApplyPositionAbility
                .Include(apa => apa.Ability)
                .Include(apa => apa.ApplyPosition)
                .Where(apa => apa.ApplyPositionId == applyPositionId);
        }
    }
}

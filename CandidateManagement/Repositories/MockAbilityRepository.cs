using CandidateManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandidateManagement.Repositories
{
    public class MockAbilityRepository : IAbilityRepository
    {
        CandidateManagementContext _context = new CandidateManagementContext();

        public IEnumerable<Ability> GetAbilities()
        {
            return _context.Ability;
        }

        public Ability GetAbility(int abilityID)
        {
            return _context.Ability.FirstOrDefault(a => a.AbilityId == abilityID);
        }
    }
}

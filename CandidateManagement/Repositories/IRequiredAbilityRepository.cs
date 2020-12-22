using CandidateManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandidateManagement.Repositories
{
    public interface IRequiredAbilityRepository
    {
        void Add(RequiredAbility requiredAbility);
        void Delete(RequiredAbility requiredAbility);
        RequiredAbility GetRequiredAbility(int requiredAbilityId);
        IEnumerable<RequiredAbility> GetRequiredAbilities(int requirementID);
    }
}

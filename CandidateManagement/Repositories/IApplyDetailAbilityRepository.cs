using CandidateManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandidateManagement.Repositories
{
    public interface IApplyDetailAbilityRepository
    {
        void Add(ApplyDetailAbility applyDetailAbility);
        void Update(ApplyDetailAbility applyDetailAbility);
        void Delete(ApplyDetailAbility applyDetailAbility);
        ApplyDetailAbility GetApplyDetailAbilityById(int applyDetailAbilityID);
        IEnumerable<ApplyDetailAbility> GetApplyDetailAbility(int applyDetailID);
    }
}

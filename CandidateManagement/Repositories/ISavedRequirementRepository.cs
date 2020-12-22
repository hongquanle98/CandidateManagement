using CandidateManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandidateManagement.Repositories
{
    public interface ISavedRequirementRepository
    {
        void Add(SavedRequirement savedRequirement);
        void Update(SavedRequirement savedRequirement);
        void Delete(SavedRequirement savedRequirement);
        IEnumerable<SavedRequirement> GetSavedRequirements(int candidateID);
        SavedRequirement GetSavedRequirement(int candidateID, int requirementID);
    }
}

using CandidateManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandidateManagement.Repositories
{
    public interface IRequirementRepository
    {
        void Add(Requirement requirement);
        void Update(Requirement requirement);
        void Delete(Requirement requirement);
        IEnumerable<Requirement> GetRequirements();
        Requirement GetRequirement(int requirementID);
        Requirement GetNewestRequirement();
        IEnumerable<Requirement> GetOtherRequirement(int requirementID);
    }
}

using CandidateManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandidateManagement.Repositories
{
    public interface IApplyPositionRepository
    {        
        IEnumerable<ApplyPosition> GetApplyPositions();
    }
}

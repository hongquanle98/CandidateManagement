using CandidateManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandidateManagement.Repositories
{
    public class MockApplyPositionRepository : IApplyPositionRepository
    {
        CandidateManagementContext _context = new CandidateManagementContext();
        public IEnumerable<ApplyPosition> GetApplyPositions()
        {
            return _context.ApplyPosition;
        }
    }
}

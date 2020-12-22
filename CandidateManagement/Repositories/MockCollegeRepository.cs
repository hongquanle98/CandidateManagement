using CandidateManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandidateManagement.Repositories
{
    public class MockCollegeRepository : ICollegeRepository
    {
        CandidateManagementContext _context = new CandidateManagementContext();
        public IEnumerable<College> GetColleges()
        {
            return _context.College;
        }
    }
}

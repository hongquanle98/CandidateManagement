using CandidateManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandidateManagement.Repositories
{
    public class MockApplyDetailAbilityRepository : IApplyDetailAbilityRepository
    {
        CandidateManagementContext _context = new CandidateManagementContext();

        public void Add(ApplyDetailAbility applyDetailAbility)
        {
            _context.ApplyDetailAbility.Add(applyDetailAbility);
            _context.SaveChanges();
        }

        public void Update(ApplyDetailAbility applyDetailAbility)
        {
            _context.ApplyDetailAbility.Update(applyDetailAbility);
            _context.SaveChanges();
        }
        public void Delete(ApplyDetailAbility applyDetailAbility)
        {
            _context.ApplyDetailAbility.Remove(applyDetailAbility);
            _context.SaveChanges();
        }
        public IEnumerable<ApplyDetailAbility> GetApplyDetailAbility(int applyDetailID)
        {
            return _context.ApplyDetailAbility
                .Include(ada => ada.ApplyDetail)
                .Include(ada => ada.Ability)
                .Where(ada => ada.ApplyDetailId == applyDetailID);
        }

        public ApplyDetailAbility GetApplyDetailAbilityById(int applyDetailAbilityID)
        {
            return _context.ApplyDetailAbility
                .Include(ada => ada.ApplyDetail)
                .Include(ada => ada.Ability)
                .FirstOrDefault(ada => ada.ApplyDetailAbilityId == applyDetailAbilityID);
        }
    }
}

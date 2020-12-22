using CandidateManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandidateManagement.Repositories
{
    public interface ICandidateRepository
    {
        void Add(Candidate candidate);
        void Update(Candidate candidate);        
        Candidate GetNewestCandidate();
        IEnumerable<Candidate> GetCandidates();                                          
        Candidate GetCandidate(int candidateID);
        Candidate GetCandidate(string userID);                              
    }
}

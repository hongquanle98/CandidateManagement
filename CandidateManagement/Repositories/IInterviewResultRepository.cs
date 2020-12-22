using CandidateManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandidateManagement.Repositories
{
    public interface IInterviewResultRepository
    {
        void Add(InterviewResult ir);
        void Update(InterviewResult ir);
        void Delete(InterviewResult ir);
        IEnumerable<InterviewResult> GetInterviewResultByInterviewID(int interviewID);
        IEnumerable<InterviewResult> GetInterviewResultByInterviewIDAndOperatorID(int interviewID, int operatorID);
    }
}

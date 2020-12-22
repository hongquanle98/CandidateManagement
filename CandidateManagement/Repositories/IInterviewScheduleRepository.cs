using CandidateManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandidateManagement.Repositories
{
    public interface IInterviewScheduleRepository
    {
        void Add(InterviewSchedule interviewSchedule);
        void Update(InterviewSchedule interviewSchedule);
        void Delete(InterviewSchedule interviewSchedule);
        InterviewSchedule GetInterviewSchedule(int interviewScheduleId);
        IEnumerable<InterviewSchedule> GetInterviewScheduleByApplyDetailId(int applyDetailID);
        IEnumerable<InterviewSchedule> GetInterviewSchedules();
        InterviewSchedule GetNewestInterviewSchedule();
    }
}

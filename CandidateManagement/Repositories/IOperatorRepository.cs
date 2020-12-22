using CandidateManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandidateManagement.Repositories
{
    public interface IOperatorRepository
    {
        void Add(Operator op);
        Operator GetOperator(int operatorID);
        Operator GetOperator(string userID);
        List<Operator> GetOperators();
    }
}

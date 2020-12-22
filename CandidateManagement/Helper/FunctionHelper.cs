using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CandidateManagement.Helper
{
    public static class FunctionHelper
    {         
        public static string GetColorClassFromApplyDetailStatus(string status)
        {
            //regex: [content] including []
            var match = Regex.Match(status, @"\[(.*?)\]");
            return match.Groups[1].Value;            
        }         
        public static string GetStatusFromApplyDetailStatus(string status)
        {
            //regex: [content] including []
            var match = Regex.Match(status, @"\[(.*?)\]");
            return status.Replace(match.Groups[0].Value, "");            
        }
    }
}

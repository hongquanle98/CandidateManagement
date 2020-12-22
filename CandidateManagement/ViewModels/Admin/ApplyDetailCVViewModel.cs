using CandidateManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace CandidateManagement.ViewModels.Admin
{
    public class ApplyDetailCVViewModel
    {
        [BindProperty]
        public Candidate Candidate { get; set; }
        [BindProperty]
        public ApplyDetail ApplyDetail { get; set; }
        [BindProperty]
        public bool CanEvaluate { get; set; }
    }
}

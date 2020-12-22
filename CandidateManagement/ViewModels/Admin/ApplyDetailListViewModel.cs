using CandidateManagement.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace CandidateManagement.ViewModels.Admin
{
    public class ApplyDetailListViewModel
    {
        [BindProperty]
        public DateTime? FromDate { get; set; }
        [BindProperty]
        public DateTime? ToDate { get; set; }
        [BindProperty]
        public string Status { get; set; }
        [BindProperty]
        public Candidate Candidate { get; set; }
        [BindProperty]
        public IEnumerable<ApplyDetail> ApplyDetails { get; set; }
    }
}

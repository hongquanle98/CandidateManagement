using CandidateManagement.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandidateManagement.ViewModels.Home
{
    public class IndexViewModel
    {
        [BindProperty]
        public IEnumerable<Requirement> Requirements { get; set; }
    }
}

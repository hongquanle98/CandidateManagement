using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CandidateManagement.Helper;
using CandidateManagement.Models;
using CandidateManagement.Repositories;
using CandidateManagement.ViewModels;
using CandidateManagement.ViewModels.Home;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CandidateManagement.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        #region dependency injection
        private readonly ICandidateRepository candidateRepository;
        private readonly IApplyDetailAbilityRepository applyDetailAbilityRepository;
        private readonly IApplyRepository applyRepository;
        private readonly IApplyDetailRepository applyDetailRepository;
        private readonly IApplyPositionAbilityRepository applyPositionAbilityRepository;
        private readonly IRequirementRepository requirementRepository;
        private readonly IRequiredAbilityRepository requiredAbilityRepository;
        private readonly ISavedRequirementRepository savedRequirementRepository;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly ILogger<HomeController> logger;

        public HomeController(ICandidateRepository candidateRepository,
                        IApplyDetailAbilityRepository applyDetailAbilityRepository,
                        IApplyRepository applyRepository,
                        IApplyDetailRepository applyDetailRepository,
                        IApplyPositionAbilityRepository applyPositionAbilityRepository,
                        IRequirementRepository requirementRepository,
                        IRequiredAbilityRepository requiredAbilityRepository,
                        ISavedRequirementRepository savedRequirementRepository,
                        UserManager<IdentityUser> userManager,
                        IHostingEnvironment hostingEnvironment,
                        SignInManager<IdentityUser> signInManager,
                        ILogger<HomeController> logger)
        {
            this.candidateRepository = candidateRepository;
            this.applyDetailAbilityRepository = applyDetailAbilityRepository;
            this.applyRepository = applyRepository;
            this.applyDetailRepository = applyDetailRepository;
            this.applyPositionAbilityRepository = applyPositionAbilityRepository;
            this.requirementRepository = requirementRepository;
            this.requiredAbilityRepository = requiredAbilityRepository;
            this.savedRequirementRepository = savedRequirementRepository;
            this.userManager = userManager;
            this.hostingEnvironment = hostingEnvironment;
            this.signInManager = signInManager;
            this.logger = logger;
        }
        #endregion

        #region Candidate Detail
        [HttpGet]
        public IActionResult InsertCandidate(string userId)
        {
            CandidateViewModel model = new CandidateViewModel()
            {
                UserId = userId
            };

            return View(model);
        }
        [HttpPost]
        public IActionResult InsertCandidate(CandidateViewModel model)
        {
            if (ModelState.IsValid)
            {
                Candidate newCandidate = new Candidate
                {
                    FullName = model.Candidate.FullName,
                    Gender = model.Candidate.Gender,
                    IdentityNumber = model.Candidate.IdentityNumber,
                    DateOfBirth = model.Candidate.DateOfBirth,
                    PlaceOfBirth = model.Candidate.PlaceOfBirth,
                    Address = model.Candidate.Address,
                    Email = model.Candidate.Email,
                    Telephone = model.Candidate.Telephone,
                    AvatarPath = "foo",
                    UserId = model.UserId
                };

                newCandidate.CandidateBackground = new CandidateBackground
                {
                    CollegeId = model.Candidate.CandidateBackground.College.CollegeId,
                    CurrentCollegeYear = model.Candidate.CandidateBackground.CurrentCollegeYear,
                    Major = model.Candidate.CandidateBackground.Major,
                    CurrentGpa = model.Candidate.CandidateBackground.CurrentGpa,
                    GraduateDate = model.Candidate.CandidateBackground.GraduateDate
                };                

                candidateRepository.Add(newCandidate);

                var newestCandidate = candidateRepository.GetNewestCandidate();

                string avatarName = string.Format("{0}_{1}", DateTime.Today.ToString("yyyy-MM-dd"), string.Format("{0:D3}", newestCandidate.CandidateId));
                newCandidate.AvatarPath = UploadFile(model.Avatar, avatarName, "candidate\\avatar");

                candidateRepository.Update(newCandidate);

                applyRepository.Add(new Apply()
                {
                    CandidateId = candidateRepository.GetNewestCandidate().CandidateId
                });

                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
        public async Task<IdentityUser> CreateCandidateUser(string email, string password)
        {
            var user = new IdentityUser
            {
                UserName = email,
                Email = email
            };
            var result = await userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                var token = await userManager.GenerateEmailConfirmationTokenAsync(user);

                var confirmationLink = Url.Action("ConfirmEmail", "Account",
                new { userId = user.Id, token = token }, Request.Scheme);

                logger.Log(LogLevel.Warning, confirmationLink);

                //await mailer.SendEmailAsync(email,
                //        "RCM - Confirmation Email",
                //        "<p>Your candidate account</p>" +
                //        $"<p>Username: {email}</p>" +
                //        $"<p>Password: {password}</p>" +
                //        $"<a href=\"{confirmationLink}\">Click here to confirm your email</a>");

                await userManager.AddToRoleAsync(user, "Candidate");

                return user;
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return null;
        }
        [HttpGet]
        public async Task<IActionResult> EditCandidate()
        {
            CandidateViewModel model = new CandidateViewModel();
            var user = userManager.GetUserAsync(User);
            var candidate = candidateRepository.GetCandidate(user.Result.Id);
            if (candidate != null)
            {
                model.Candidate = candidate;
                //CVFile = ReturnFormFile(GetFileFromPath(Candidate.CandidateBackground.CvfilePath, "candidate-cv"));
                //Avatar = ReturnFormFile(GetFileFromPath(Candidate.AvatarPath, "candidate-avatar"));
                return View(model);
            }
            string ErrorTitle = "Error";
            string ErrorMessage = "Error while get candidate detail";
            return RedirectToAction("Error", new { ErrorTitle = ErrorTitle, ErrorMessage = ErrorMessage });
        }
        [HttpGet]
        public async Task<IActionResult> CandidateById(int id)
        {
            CandidateViewModel model = new CandidateViewModel();
            var candidate = candidateRepository.GetCandidate(id);
            if (candidate != null)
            {
                model.Candidate = candidate;
                //CVFile = ReturnFormFile(GetFileFromPath(Candidate.CandidateBackground.CvfilePath, "candidate-cv"));
                //Avatar = ReturnFormFile(GetFileFromPath(Candidate.AvatarPath, "candidate-avatar"));
                return View(model);
            }
            string ErrorTitle = "Error";
            string ErrorMessage = "Error while get candidate detail";
            return RedirectToAction("Error", new { ErrorTitle = ErrorTitle, ErrorMessage = ErrorMessage });
        }
        [HttpPost]
        public IActionResult EditCandidate(CandidateViewModel model)
        {
            //string avatarPath = UploadFile(model.Avatar, model.Candidate.ApplyDetail.CandidateNo, "candidate-avatar");
            //string cvFilePath = UploadFile(model.CVFile, model.Candidate.ApplyDetail.CandidateNo, "candidate-cv");
            //if (avatarPath != null)
            //{
            //    model.Candidate.AvatarPath = avatarPath;
            //}
            //if (cvFilePath != null)
            //{
            //    model.Candidate.CandidateBackground.CvfilePath = cvFilePath;
            //}
            candidateRepository.Update(model.Candidate);

            return View(model);
        }        
        public string UploadFile(IFormFile file, string preFileName, string desFolder)
        {
            string uniqueFileName = null;
            if (file != null)
            {
                // The image must be uploaded to the images folder in wwwroot
                // To get the path of the wwwroot folder we are using the inject
                // HostingEnvironment service provided by ASP.NET Core
                string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, desFolder);
                // To make sure the file name is unique we are appending a new
                // GUID value and and an underscore to the file name
                uniqueFileName = preFileName + "_" + file.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                // Use CopyTo() method provided by IFormFile interface to
                // copy the file to wwwroot/images folder
                file.CopyTo(new FileStream(filePath, FileMode.Create));
            }
            return uniqueFileName;
        }
        #endregion

        #region Index
        [HttpGet]
        public IActionResult Index()
        {
            IndexViewModel model = new IndexViewModel()
            {
                Requirements = requirementRepository.GetRequirements()
            };
            return View(model);
        }
        [HttpGet]
        public IActionResult RequirementDetail(int id)
        {
            Requirement requirement = requirementRepository.GetRequirement(id);
            requirement.ViewCount += 1;
            requirementRepository.Update(requirement);

            RequirementDetailViewModel model = new RequirementDetailViewModel()
            {
                Requirement = requirementRepository.GetRequirement(id),
                OtherRequirement = requirementRepository.GetOtherRequirement(id)
            };
            if (signInManager.IsSignedIn(User) && User.IsInRole("Candidate"))
            {
                model.Candidate = candidateRepository.GetCandidate(userManager.GetUserId(User));
            }
            else
            {
                model.Candidate = new Candidate();
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult ApplyRequirementDetail(int requirementId, int candidateId)
        {
            Task.Delay(TimeSpan.FromSeconds(ConstantHelper.Submit.Delay)).Wait();

            ApplyRequirementDetailViewModel model = new ApplyRequirementDetailViewModel()
            {
                Requirement = requirementRepository.GetRequirement(requirementId),
                Candidate = candidateRepository.GetCandidate(candidateId)
            };
            var applyPositionAbilityList = applyPositionAbilityRepository.GetApplyPositionAbilities(model.Requirement.ApplyPositionId).Select(apa => apa.Ability);
            var requiredAbilityList = requiredAbilityRepository.GetRequiredAbilities(model.Requirement.RequirementId).Where(ra => !new string[] { "Salary", "WorkedTime"}.Contains(ra.Ability.Note)).Select(ra => ra.Ability);
            var advantageAbilityList = applyPositionAbilityList.Where(a => !requiredAbilityList.Select(a1 => a1.AbilityId).Contains(a.AbilityId));
            model.RequiredAbilities = new List<ApplyRequirementDetailViewModel.Ability>();
            foreach (var ability in requiredAbilityList)
            {
                var ab = new ApplyRequirementDetailViewModel.Ability
                {
                    AbilityID = ability.AbilityId,
                    AbilityName = ability.AbilityName,
                    IsSelected = false
                };

                model.RequiredAbilities.Add(ab);
            }
            model.AdvantageAbilities = new List<ApplyRequirementDetailViewModel.Ability>();
            foreach (var ability in advantageAbilityList)
            {
                var ab = new ApplyRequirementDetailViewModel.Ability
                {
                    AbilityID = ability.AbilityId,
                    AbilityName = ability.AbilityName,
                    IsSelected = false
                };

                model.AdvantageAbilities.Add(ab);
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> ApplyRequirementDetailAsync(ApplyRequirementDetailViewModel model)
        {
            Task.Delay(TimeSpan.FromSeconds(ConstantHelper.Submit.Delay)).Wait();

            try
            {
                var applyDetail = new ApplyDetail
                {
                    ApplyId = applyRepository.GetApplyByCandidateId(model.Candidate.CandidateId).ApplyId,
                    RequirementId = model.Requirement.RequirementId,
                    ApplyDate = DateTime.Now,
                    ExpectedSalary = model.ApplyDetail.ExpectedSalary,
                    WorkedTime = model.ApplyDetail.WorkedTime,
                    CvfilePath = "foo"
                };

                applyDetailRepository.Add(applyDetail);
                var newestApplyDetail = applyDetailRepository.GetNewestApplyDetail();

                string cvFileName = string.Format("{0}_{1}", applyDetail.ApplyDate.Date.ToString("yyyy-MM-dd"), string.Format("{0:D3}", newestApplyDetail.ApplyDetailId));
                applyDetail.CvfilePath = UploadFile(model.CVFile, cvFileName, "candidate\\cv");

                applyDetailRepository.Update(applyDetail);

                foreach (var ability in model.RequiredAbilities.Union(model.AdvantageAbilities).Where(a => a.IsSelected))
                {
                    var applyDetailAbility = new ApplyDetailAbility
                    {
                        ApplyDetailId = applyDetailRepository.GetNewestApplyDetail().ApplyDetailId,
                        AbilityId = ability.AbilityID,
                        Point = ability.Point
                    };
                    applyDetailAbilityRepository.Add(applyDetailAbility);
                }
            }
            catch (Exception)
            {
                return Json(new
                {
                    success = false,
                    title = "Error",
                    message = "Error while applying"
                });
            }
            return Json(new
            {
                success = true,
                title = "Success",
                message = "Apply successed",
                dataApplyRequirementButtonPartial = await this.RenderViewAsync("_ApplyRequirementButtonPartial",
                                                                            new ApplyOrSaveRequirementButtonViewModel
                                                                            {
                                                                                Candidate = model.Candidate,
                                                                                Requirement = model.Requirement
                                                                            },
                                                                            true),
                dataAppliedRequirementPartial = await this.RenderViewAsync("_AppliedRequirementPartial",
                                                                        new CandidateRequirementViewModel
                                                                        {
                                                                            Candidate = model.Candidate,
                                                                            SavedRequirements = savedRequirementRepository.GetSavedRequirements(model.Candidate.CandidateId).Select(sr => sr.Requirement),
                                                                            AppliedRequirements = applyDetailRepository.GetApplyDetailsByCandidateId(model.Candidate.CandidateId).Select(sr => sr.Requirement)
                                                                        },
                                                                        true)
            });
        }
        [HttpPost]
        public async Task<IActionResult> SaveRequirementAsync(int candidateId, int requirementId, string saving, string requirementDetail)
        {
            bool isSaving = saving.Equals("true");
            bool isRequirementDetail = requirementDetail.Equals("true");
            Task.Delay(TimeSpan.FromSeconds(ConstantHelper.Submit.Delay)).Wait();

            try
            {
                var savedRequirement = savedRequirementRepository.GetSavedRequirement(candidateId, requirementId);
                if (savedRequirement != null)
                    savedRequirementRepository.Delete(savedRequirement);
                else
                    savedRequirementRepository.Add(new SavedRequirement
                    {
                        CandidateId = candidateId,
                        RequirementId = requirementId
                    });
            }
            catch (Exception)
            {
                return Json(new
                {
                    success = false,
                    title = "Error",
                    message = isSaving ? "Save requirement failed" : "Unsave requirement failed"
                });
            }
            var candidate = candidateRepository.GetCandidate(candidateId);
            return Json(new
            {
                success = true,
                title = "Success",
                message = isSaving ? "Requirement saved" : "Requirement unsaved",
                dataSaveRequirementButtonPartial = await this.RenderViewAsync("_SaveRequirementButtonPartial",
                                                                            new ApplyOrSaveRequirementButtonViewModel
                                                                            {
                                                                                Candidate = candidate,
                                                                                Requirement = requirementRepository.GetRequirement(requirementId),
                                                                                IsRequirementDetail = isRequirementDetail
                                                                            },
                                                                            true),
                dataSavedRequirementPartial = await this.RenderViewAsync("_SavedRequirementPartial",
                                                                        new CandidateRequirementViewModel
                                                                        {
                                                                            Candidate = candidate,
                                                                            SavedRequirements = savedRequirementRepository.GetSavedRequirements(candidate.CandidateId).Select(sr => sr.Requirement),
                                                                            AppliedRequirements = applyDetailRepository.GetApplyDetailsByCandidateId(candidate.CandidateId).Select(sr => sr.Requirement)
                                                                        },
                                                                        true)
            });
        }
        [HttpGet]
        public IActionResult CandidateRequirement()
        {
            var candidate = candidateRepository.GetCandidate(userManager.GetUserId(User));
            CandidateRequirementViewModel model = new CandidateRequirementViewModel
            {
                Candidate = candidate,
                SavedRequirements = savedRequirementRepository.GetSavedRequirements(candidate.CandidateId).Select(sr => sr.Requirement),
                AppliedRequirements = applyDetailRepository.GetApplyDetailsByCandidateId(candidate.CandidateId).Select(sr => sr.Requirement)
            };
            return View(model);
        }
        #endregion        
    }
}
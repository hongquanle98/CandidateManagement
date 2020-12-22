using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CandidateManagement.Helper;
using CandidateManagement.Models;
using CandidateManagement.Repositories;
using CandidateManagement.ViewModels.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CandidateManagement.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        #region dependency injection
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly IAbilityRepository abilityRepository;
        private readonly IApplyDetailRepository applyDetailRepository;
        private readonly IApplyPositionAbilityRepository applyPositionAbilityRepository;
        private readonly IApplyPositionRepository applyPositionRepository;
        private readonly IApplyDetailAbilityRepository applyDetailAbilityRepository;
        private readonly ICandidateRepository candidateRepository;
        private readonly IInterviewScheduleRepository interviewScheduleRepository;
        private readonly IInterviewResultRepository interviewResultRepository;
        private readonly IRequirementRepository requirementRepository;
        private readonly IRequiredAbilityRepository requiredAbilityRepository;
        private readonly IOperatorRepository operatorRepository;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly ILogger<AdminController> logger;

        public AdminController(IHostingEnvironment hostingEnvironment,
                            IAbilityRepository abilityRepository,
                            IApplyDetailRepository applyDetailRepository,
                            IApplyPositionAbilityRepository applyPositionAbilityRepository,
                            IApplyPositionRepository applyPositionRepository,
                            IApplyDetailAbilityRepository applyDetailAbilityRepository,
                            ICandidateRepository candidateRepository,
                            IInterviewScheduleRepository interviewScheduleRepository,
                            IInterviewResultRepository interviewResultRepository,
                            IRequirementRepository requirementRepository,
                            IRequiredAbilityRepository requiredAbilityRepository,
                            IOperatorRepository operatorRepository,
                            RoleManager<IdentityRole> roleManager,
                            UserManager<IdentityUser> userManager,
                            SignInManager<IdentityUser> signInManager,
                            ILogger<AdminController> logger)
        {
            this.hostingEnvironment = hostingEnvironment;
            this.abilityRepository = abilityRepository;
            this.applyDetailRepository = applyDetailRepository;
            this.applyPositionAbilityRepository = applyPositionAbilityRepository;
            this.applyPositionRepository = applyPositionRepository;
            this.applyDetailAbilityRepository = applyDetailAbilityRepository;
            this.candidateRepository = candidateRepository;
            this.interviewScheduleRepository = interviewScheduleRepository;
            this.interviewResultRepository = interviewResultRepository;
            this.requirementRepository = requirementRepository;
            this.requiredAbilityRepository = requiredAbilityRepository;
            this.operatorRepository = operatorRepository;
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
        }
        #endregion

        #region Interview Schedule
        [HttpGet]
        public IActionResult AddInterviewSchedule(int id)
        {
            var applyDetail = applyDetailRepository.GetApplyDetail(id);
            InterviewScheduleViewModel model = new InterviewScheduleViewModel()
            {
                Candidate = candidateRepository.GetCandidate(applyDetail.Apply.CandidateId),
                ApplyDetail = applyDetail
            };
            var operators = operatorRepository.GetOperators();
            model.Operators = new List<InterviewScheduleViewModel.Operator>();
            foreach (var op in operators)
            {
                var o = new InterviewScheduleViewModel.Operator
                {
                    OperatorID = op.OperatorId,
                    OperatorName = op.OperatorName,
                    IsSelected = false
                };
                model.Operators.Add(o);
            }

            model.MailSubject = string.Format("THƯ MỜI ĐẾN PHỎNG VẤN - {0}", ConstantHelper.Company.Name);

            string templateFolder = Path.Combine(hostingEnvironment.WebRootPath, "email-template/interview");
            FileInfo[] files = new DirectoryInfo(templateFolder).GetFiles("*.html");

            model.EmailTemplates = new List<EmailTemplate>();
            foreach (var file in files)
            {
                EmailTemplate emailTemplate = new EmailTemplate
                {
                    FileName = file.Name,
                    FullPath = file.FullName
                };
                model.EmailTemplates.Add(emailTemplate);
            }
            return PartialView("_AddInterviewSchedulePartial", model);

        }
        [HttpPost]
        public IActionResult AddInterviewSchedule(InterviewScheduleViewModel model)
        {
            try
            {
                var interviewSchedule = new InterviewSchedule
                {
                    ApplyDetailId = model.ApplyDetail.ApplyDetailId,
                    InterviewDate = model.InterviewSchedule.InterviewDate,
                    InterviewTime = model.InterviewSchedule.InterviewTime,
                    IsEmailSent = false
                };
                interviewScheduleRepository.Add(interviewSchedule);
                foreach (var op in model.Operators.Where(tp => tp.IsSelected))
                {
                    var applyDetailAbilities = applyDetailAbilityRepository.GetApplyDetailAbility(model.ApplyDetail.ApplyDetailId);
                    foreach (var applyDetailAbility in applyDetailAbilities)
                    {
                        var ir = new InterviewResult
                        {
                            InterviewId = interviewScheduleRepository.GetNewestInterviewSchedule().InterviewId,
                            OperatorId = op.OperatorID,
                            ApplyDetailAbilityId = applyDetailAbility.ApplyDetailAbilityId
                        };
                        interviewResultRepository.Add(ir);
                    }
                }
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
            return Redirect(Request.Headers["Referer"].ToString());
        }
        [HttpPost]
        public async Task<IActionResult> SendAddInterviewMail(InterviewScheduleViewModel model)
        {
            try
            {
                var interviewSchedule = new InterviewSchedule
                {
                    ApplyDetailId = model.ApplyDetail.ApplyDetailId,
                    InterviewDate = model.InterviewSchedule.InterviewDate,
                    InterviewTime = model.InterviewSchedule.InterviewTime,
                    IsEmailSent = false
                };
                interviewScheduleRepository.Add(interviewSchedule);
                foreach (var op in model.Operators.Where(tp => tp.IsSelected))
                {
                    var applyDetailAbilities = applyDetailAbilityRepository.GetApplyDetailAbility(model.ApplyDetail.ApplyDetailId);
                    foreach (var applyDetailAbility in applyDetailAbilities)
                    {
                        var ir = new InterviewResult
                        {
                            InterviewId = interviewScheduleRepository.GetNewestInterviewSchedule().InterviewId,
                            OperatorId = op.OperatorID,
                            ApplyDetailAbilityId = applyDetailAbility.ApplyDetailAbilityId
                        };
                        interviewResultRepository.Add(ir);
                    }
                }
                await SendInterviewEmail(model);
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
            return Redirect(Request.Headers["Referer"].ToString());
        }
        [HttpGet]
        public IActionResult EditInterviewSchedule(int applyDetailId, int interviewId)
        {
            var applyDetail = applyDetailRepository.GetApplyDetail(applyDetailId);
            InterviewScheduleViewModel model = new InterviewScheduleViewModel()
            {
                Candidate = candidateRepository.GetCandidate(applyDetail.Apply.CandidateId),
                ApplyDetail = applyDetail,
                InterviewSchedule = interviewScheduleRepository.GetInterviewSchedule(interviewId)
            };
            var operators = operatorRepository.GetOperators();
            model.Operators = new List<InterviewScheduleViewModel.Operator>();

            foreach (var op in operators)
            {
                var o = new InterviewScheduleViewModel.Operator
                {
                    OperatorID = op.OperatorId,
                    OperatorName = op.OperatorName
                };
                if (interviewResultRepository.GetInterviewResultByInterviewID(interviewId).Where(iso => iso.OperatorId == o.OperatorID).Any())
                    o.IsSelected = true;
                else
                    o.IsSelected = false;

                var irOfOperator = interviewResultRepository.GetInterviewResultByInterviewID(interviewId).Where(ir => ir.OperatorId == op.OperatorId && ir.Point > 0);
                if (irOfOperator.Any())
                {
                    o.HasEvaluated = true;
                }

                model.Operators.Add(o);
            }

            model.MailSubject = string.Format("THƯ MỜI ĐẾN PHỎNG VẤN - {0}", ConstantHelper.Company.Name);

            string templateFolder = Path.Combine(hostingEnvironment.WebRootPath, "email-template/interview");
            FileInfo[] files = new DirectoryInfo(templateFolder).GetFiles("*.html");

            model.EmailTemplates = new List<EmailTemplate>();
            foreach (var file in files)
            {
                EmailTemplate emailTemplate = new EmailTemplate
                {
                    FileName = file.Name,
                    FullPath = file.FullName
                };
                model.EmailTemplates.Add(emailTemplate);
            }
            return PartialView("_EditInterviewSchedulePartial", model);
        }
        [HttpPost]
        public IActionResult EditInterviewSchedule(InterviewScheduleViewModel model)
        {
            //try
            //{
            foreach (var iso in interviewResultRepository.GetInterviewResultByInterviewID(model.InterviewSchedule.InterviewId).ToList())
            {
                interviewResultRepository.Delete(iso);
            }

            foreach (var op in model.Operators.Where(o => o.IsSelected))
            {
                var applyDetailAbilities = applyDetailAbilityRepository.GetApplyDetailAbility(model.ApplyDetail.ApplyDetailId);
                foreach (var applyDetailAbility in applyDetailAbilities)
                {
                    var iso = new InterviewResult
                    {
                        InterviewId = model.InterviewSchedule.InterviewId,
                        OperatorId = op.OperatorID,
                        ApplyDetailAbilityId = applyDetailAbility.ApplyDetailAbilityId
                    };
                    interviewResultRepository.Add(iso);
                }
            }
            model.InterviewSchedule.IsEmailSent = false;
            interviewScheduleRepository.Update(model.InterviewSchedule);
            //}
            //catch (Exception)
            //{
            //    return RedirectToPage("/Error", new { ErrorTitle = "Error", ErrorMessage = "Send Email Error" });
            //}
            return Redirect(Request.Headers["Referer"].ToString());
        }
        public async Task<IActionResult> SendEditInterviewMail(InterviewScheduleViewModel model)
        {
            try
            {
                foreach (var iso in interviewResultRepository.GetInterviewResultByInterviewID(model.InterviewSchedule.InterviewId).ToList())
                {
                    interviewResultRepository.Delete(iso);
                }

                foreach (var op in model.Operators.Where(o => o.IsSelected))
                {
                    var iso = new InterviewResult
                    {
                        InterviewId = model.InterviewSchedule.InterviewId,
                        OperatorId = op.OperatorID
                    };
                    interviewResultRepository.Add(iso);
                }
                await SendInterviewEmail(model);
                model.InterviewSchedule.IsEmailSent = true;
                interviewScheduleRepository.Update(model.InterviewSchedule);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", new { ErrorTitle = "Error", ErrorMessage = "Send Email Error" });
            }

            return RedirectToAction("ScheduleHistory", "Admin", new { id = model.Candidate.CandidateId });
        }
        public async Task SendInterviewEmail(InterviewScheduleViewModel model)
        {
            model.MailBody = System.IO.File.ReadAllText(model.EmailTemplate.FullPath);
            model.MailBody = model.MailBody.Replace("timeText", string.Format("{0:hh\\:mm}", model.InterviewSchedule.InterviewTime));
            model.MailBody = model.MailBody.Replace("dayText", (((int)model.InterviewSchedule.InterviewDate.DayOfWeek + 1) == 1 ? "ngày Chủ Nhật" : "ngày thứ " + ((int)model.InterviewSchedule.InterviewDate.DayOfWeek + 1)));
            model.MailBody = model.MailBody.Replace("dateText", string.Format("{0:hh\\:mm}", model.InterviewSchedule.InterviewDate.ToString("dd-MM-yyyy")));
            //await mailer.SendEmailAsync(model.Candidate.Email, model.MailSubject, model.MailBody);
        }
        #endregion

        #region Candidate
        [HttpGet]
        public IActionResult CandidateEvaluate()
        {
            return View();
        }
        [HttpGet]
        public IActionResult CandidateList()
        {
            CandidateListViewModel model = new CandidateListViewModel();
            model.Candidates = candidateRepository.GetCandidates();
            return View(model);
        }
        [HttpPost]
        public IActionResult CandidateListPost(CandidateListViewModel model)
        {
            return View(model);
        }
        [HttpGet]
        public IActionResult CandidateApplyDetailList(int id)
        {
            ApplyDetailListViewModel model = new ApplyDetailListViewModel
            {
                Candidate = candidateRepository.GetCandidate(id),
                ApplyDetails = applyDetailRepository.GetApplyDetailsByCandidateId(id)
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult CandidateApplyDetailList(ApplyDetailListViewModel model)
        {
            if (model.FromDate.HasValue && model.ToDate.HasValue)
            {
                model.ApplyDetails = applyDetailRepository.GetApplyDetailsByCandidateId(model.Candidate.CandidateId)
                        .Where(ad => ad.ApplyDate.Date >= model.FromDate.Value.Date && ad.ApplyDate.Date <= model.ToDate.Value.Date);
            }
            else
            {
                model.ApplyDetails = applyDetailRepository.GetApplyDetailsByCandidateId(model.Candidate.CandidateId);
            }
            switch (model.Status)
            {
                case "CVPending":
                    {
                        model.ApplyDetails = model.ApplyDetails.Where(ad => (ad.IsCvpass == null));
                        break;
                    }
                case "CVPassed":
                    {
                        model.ApplyDetails = model.ApplyDetails.Where(ad => (ad.IsCvpass == true));
                        break;
                    }
                case "CVFailed":
                    {
                        model.ApplyDetails = model.ApplyDetails.Where(ad => (ad.IsCvpass == false));
                        break;
                    }
                case "InterviewNotScheduled":
                    {
                        model.ApplyDetails = model.ApplyDetails.Where(ad => (ad.IsInterviewPass == null
                                                      && ad.IsCvpass == true));
                        break;
                    }
                case "InterviewPassed":
                    {
                        model.ApplyDetails = model.ApplyDetails.Where(ad => (ad.IsInterviewPass == true));
                        break;
                    }
                case "InterviewFailed":
                    {
                        model.ApplyDetails = model.ApplyDetails.Where(ad => (ad.IsInterviewPass == false));
                        break;
                    }
                default:
                    break;
            }
            return View(model);
        }
        #endregion

        #region Apply Detail
        [HttpGet]
        public IActionResult ShowApplyDetailCV(int candidateId, int applyDetailId)
        {
            ApplyDetailCVViewModel model = new ApplyDetailCVViewModel()
            {
                Candidate = candidateRepository.GetCandidate(candidateId),
                ApplyDetail = applyDetailRepository.GetApplyDetail(applyDetailId)
            };
            return PartialView("_ApplyDetailCVPartial", model);
        }

        #endregion

        #region Role
        [HttpGet]
        public IActionResult ListRoles()
        {
            ListRolesViewModel model = new ListRolesViewModel
            {
                ListRoles = roleManager.Roles
            };
            return View(model);
        }
        [HttpGet]
        public IActionResult CreateRole()
        {
            CreateRoleViewModel model = new CreateRoleViewModel();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> CreateRolePost(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                // We just need to specify a unique role name to create a new role
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RoleName
                };

                // Saves the role in the underlying AspNetRoles table
                IdentityResult result = await roleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            // Find the role by Role ID
            var role = await roleManager.FindByIdAsync(id);

            if (role == null)
            {
                string ErrorMessage = $"Role with Id = {id} cannot be found";
                return RedirectToPage("/NotFound", new { ErrorMessage = ErrorMessage });
            }

            EditRoleViewModel model = new EditRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name
            };

            // Retrieve all the Users
            foreach (var user in userManager.Users)
            {
                // If the user is in this role, add the username to
                // Users property of EditRoleViewModel. This model
                // object is then passed to the view for display
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            var role = await roleManager.FindByIdAsync(model.Id);

            if (role == null)
            {
                string ErrorMessage = $"Role with Id = {model.Id} cannot be found";
                return RedirectToAction("NotFound", new { ErrorMessage = ErrorMessage });
            }
            else
            {
                role.Name = model.RoleName;

                // Update the Role using UpdateAsync
                var result = await roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles", "Admin");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return View(model);
            }
        }
        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await roleManager.FindByIdAsync(id);

            if (role == null)
            {
                string ErrorMessage = $"Role with Id = {id} cannot be found";
                return RedirectToAction("NotFound", new { ErrorMessage = ErrorMessage });
            }
            else
            {
                // Wrap the code in a try/catch block
                try
                {
                    //throw new Exception("Test Exception");

                    var result = await roleManager.DeleteAsync(role);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("ListRoles", "Admin");
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }

                    return RedirectToAction("ListRoles", "Admin");
                }
                // If the exception is DbUpdateException, we know we are not able to
                // delete the role as there are users in the role being deleted
                catch (DbUpdateException ex)
                {
                    //Log the exception to a file. We discussed logging to a file
                    // using Nlog in Part 63 of ASP.NET Core tutorial
                    //logger.LogError($"Exception Occured : {ex}");
                    // Pass the ErrorTitle and ErrorMessage that you want to show to
                    // the user using ViewBag. The Error view retrieves this data
                    // from the ViewBag and displays to the user.
                    logger.Log(LogLevel.Error, ex.ToString());
                    string ErrorTitle = $"{role.Name} role is in use";
                    string ErrorMessage = $"{role.Name} role cannot be deleted as there are users in this role. If you want to delete this role, please remove the users from the role and then try to delete";
                    return RedirectToAction("Error", new { ErrorTitle = ErrorTitle, ErrorMessage = ErrorMessage });
                }
            }
        }
        [HttpGet]
        public async Task<IActionResult> ManageUserRoles(string userId)
        {
            ManageUserRolesViewModel model = new ManageUserRolesViewModel();
            model.UserId = userId;

            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                string ErrorMessage = $"User with Id = {userId} cannot be found";
                return RedirectToAction("NotFound", new { ErrorMessage = ErrorMessage });
            }

            model.ListUserRoles = new List<ManageUserRolesViewModel.Role>();

            foreach (var r in roleManager.Roles)
            {
                var role = new ManageUserRolesViewModel.Role
                {
                    RoleId = r.Id,
                    RoleName = r.Name
                };

                if (await userManager.IsInRoleAsync(user, r.Name))
                {
                    role.IsSelected = true;
                }
                else
                {
                    role.IsSelected = false;
                }

                model.ListUserRoles.Add(role);
            }

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> ManageUserRoles(ManageUserRolesViewModel model)
        {
            var user = await userManager.FindByIdAsync(model.UserId);

            if (user == null)
            {
                string ErrorMessage = $"User with Id = {model.UserId} cannot be found";
                return RedirectToAction("NotFound", new { ErrorMessage = ErrorMessage });
            }

            var roles = await userManager.GetRolesAsync(user);
            var result = await userManager.RemoveFromRolesAsync(user, roles);

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Cannot remove user existing roles");
                return View(model);
            }

            result = await userManager.AddToRolesAsync(user,
                model.ListUserRoles.Where(x => x.IsSelected).Select(y => y.RoleName));

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Cannot add selected roles to user");
                return View(model);
            }

            return RedirectToAction("EditUser", "Admin", new { Id = model.UserId });
        }
        #endregion

        #region User
        [HttpGet]
        public IActionResult ListUsers()
        {
            ListUsersViewModel model = new ListUsersViewModel
            {
                ListUsers = userManager.Users
            };
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null)
            {
                string ErrorMessage = $"User with Id = {id} cannot be found";
                return RedirectToPage("/NotFound", new { ErrorMessage = ErrorMessage });
            }

            // GetClaimsAsync retunrs the list of user Claims
            var userClaims = await userManager.GetClaimsAsync(user);
            // GetRolesAsync returns the list of user Roles
            var userRoles = await userManager.GetRolesAsync(user);

            EditUserViewModel model = new EditUserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                Claims = userClaims.Select(c => c.Value).ToList(),
                Roles = userRoles
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            var user = await userManager.FindByIdAsync(model.Id);

            if (user == null)
            {
                string ErrorMessage = $"User with Id = {model.Id} cannot be found";
                return RedirectToPage("/NotFound", new { ErrorMessage = ErrorMessage });
            }
            else
            {
                user.Email = model.Email;
                user.UserName = model.UserName;

                var result = await userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListUsers", "Admin");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return View(model);
            }
        }
        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(string roleId)
        {
            EditUsersInRoleViewModel model = new EditUsersInRoleViewModel();
            model.RoleId = roleId;

            var role = await roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                string ErrorMessage = $"Role with Id = {roleId} cannot be found";
                return RedirectToPage("/NotFound", new { ErrorMessage = ErrorMessage });
            }

            model.ListUserInRole = new List<EditUsersInRoleViewModel.User>();

            foreach (var u in userManager.Users)
            {
                model.user = new EditUsersInRoleViewModel.User
                {
                    UserId = u.Id,
                    UserName = u.UserName
                };

                if (await userManager.IsInRoleAsync(u, role.Name))
                {
                    model.user.IsSelected = true;
                }
                else
                {
                    model.user.IsSelected = false;
                }

                model.ListUserInRole.Add(model.user);
            }

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(EditUsersInRoleViewModel model)
        {
            var role = await roleManager.FindByIdAsync(model.RoleId);

            if (role == null)
            {
                string ErrorMessage = $"Role with Id = {model.RoleId} cannot be found";
                return RedirectToPage("/NotFound", new { ErrorMessage = ErrorMessage });
            }

            for (int i = 0; i < model.ListUserInRole.Count; i++)
            {
                var user = await userManager.FindByIdAsync(model.ListUserInRole[i].UserId);

                IdentityResult result = null;

                if (model.ListUserInRole[i].IsSelected && !(await userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!model.ListUserInRole[i].IsSelected && await userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }

                if (result.Succeeded)
                {
                    if (i < (model.ListUserInRole.Count - 1))
                        continue;
                    else
                        return RedirectToAction("EditRole", "Admin", new { Id = model.RoleId });
                }
            }

            return RedirectToAction("EditRole", "Admin", new { Id = model.RoleId });
        }
        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null)
            {
                string ErrorMessage = $"User with Id = {id} cannot be found";
                return RedirectToAction("NotFound", new { ErrorMessage = ErrorMessage });
            }
            else
            {
                var result = await userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListUsers", "Admin");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return View();
            }
        }
        #endregion

        #region User Claims       
        [HttpGet]
        public async Task<IActionResult> ManageUserClaims(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                string ErrorMessage = $"User with Id = {userId} cannot be found";
                return RedirectToAction("/NotFound", new { ErrorMessage = ErrorMessage });
            }

            // UserManager service GetClaimsAsync method gets all the current claims of the user
            var existingUserClaims = await userManager.GetClaimsAsync(user);

            ManageUserClaimsViewModel model = new ManageUserClaimsViewModel();

            model.UserId = userId;
            model.ListCliams = new List<ManageUserClaimsViewModel.UserClaim>();

            // Loop through each claim we have in our application
            foreach (Claim claim in ClaimsStore.AllClaims)
            {
                ManageUserClaimsViewModel.UserClaim userClaim = new ManageUserClaimsViewModel.UserClaim
                {
                    ClaimType = claim.Type
                };

                // If the user has the claim, set IsSelected property to true, so the checkbox
                // next to the claim is checked on the UI
                if (existingUserClaims.Any(c => c.Type == claim.Type))
                {
                    userClaim.IsSelected = true;
                }

                model.ListCliams.Add(userClaim);
            }

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> ManageUserClaims(ManageUserClaimsViewModel model)
        {
            var user = await userManager.FindByIdAsync(model.UserId);

            if (user == null)
            {
                string ErrorMessage = $"User with Id = {model.UserId} cannot be found";
                return RedirectToAction("NotFound", new { ErrorMessage = ErrorMessage });
            }

            // Get all the user existing claims and delete them
            var claims = await userManager.GetClaimsAsync(user);
            var result = await userManager.RemoveClaimsAsync(user, claims);

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Cannot remove user existing claims");
                return View(model);
            }

            // Add all the claims that are selected on the UI
            result = await userManager.AddClaimsAsync(user,
                model.ListCliams.Where(c => c.IsSelected).Select(c => new Claim(c.ClaimType, c.ClaimType)));

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Cannot add selected claims to user");
                return View(model);
            }

            return RedirectToAction("EditUser", "Admin", new { Id = model.UserId });

        }
        #endregion

        #region Schedule History
        [HttpGet]
        public IActionResult ScheduleHistory(int id)
        {
            var applyDetail = applyDetailRepository.GetApplyDetail(id);
            ScheduleHistoryViewModel model = new ScheduleHistoryViewModel()
            {
                Candidate = candidateRepository.GetCandidate(applyDetail.Apply.CandidateId),
                ApplyDetail = applyDetail,
                InterviewSchedules = interviewScheduleRepository.GetInterviewScheduleByApplyDetailId(id)
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult DeleteInterview(int id, ScheduleHistoryViewModel model)
        {
            var interviewSchedule = interviewScheduleRepository.GetInterviewSchedule(id);
            interviewScheduleRepository.Delete(interviewSchedule);
            return Redirect(Request.Headers["Referer"].ToString());
        }

        #endregion

        #region Requirement
        [HttpGet]
        public IActionResult RequirementList()
        {
            RequirementListViewModel model = new RequirementListViewModel()
            {
                Requirements = requirementRepository.GetRequirements()
            };
            return View(model);
        }
        [HttpGet]
        public IActionResult AddRequirement()
        {
            RequirementViewModel model = new RequirementViewModel();
            var applyPositions = applyPositionRepository.GetApplyPositions();
            var applyPositionsAbilities = applyPositionAbilityRepository.GetApplyPositionAbilities(applyPositions.Select(ap => ap.ApplyPositionId).FirstOrDefault()).Select(apa => apa.Ability);
            model.ApplyPositionAbilities = new List<RequirementViewModel.Ability>();
            foreach (var ability in applyPositionsAbilities)
            {
                var ab = new RequirementViewModel.Ability
                {
                    AbilityID = ability.AbilityId,
                    AbilityName = ability.AbilityName,
                    IsSelected = false
                };
                model.ApplyPositionAbilities.Add(ab);
            }
            model.SalaryAbilities = abilityRepository.GetAbilities().Where(a => a.Note == "Salary").ToList();
            model.SelectedSalaryAbilitiyId = model.SalaryAbilities.FirstOrDefault(a => a.AbilityName == "Thương lượng").AbilityId;
            model.WorkedTimeAbilities = abilityRepository.GetAbilities().Where(a => a.Note == "WorkedTime").ToList();
            model.SelectedWorkedTimeAbilitiyId = model.WorkedTimeAbilities.FirstOrDefault(a => a.AbilityName == "Không cần kinh nghiệm").AbilityId;
            return View(model);
        }
        [HttpPost]
        public IActionResult AddRequirement(RequirementViewModel model)
        {
            try
            {
                var requirement = new Requirement
                {
                    Title = model.Requirement.Title,
                    Description = model.Requirement.Description,
                    RequireDescription = model.Requirement.RequireDescription,
                    WorkPlace = model.Requirement.WorkPlace,
                    Language = model.Requirement.Language,
                    RecruitFrom = model.Requirement.RecruitFrom.Date,
                    RecruitTo = model.Requirement.RecruitTo.Date,
                    ApplyPositionId = model.Requirement.ApplyPositionId,
                    RecruitAmount = model.Requirement.RecruitAmount,
                    ViewCount = 0,
                    CreateTime = DateTime.Now
                };
                requirementRepository.Add(requirement);
                foreach (var ability in model.ApplyPositionAbilities.Where(tp => tp.IsSelected))
                {
                    var ab = new RequiredAbility
                    {
                        RequirementId = requirementRepository.GetNewestRequirement().RequirementId,
                        AbilityId = ability.AbilityID
                    };
                    requiredAbilityRepository.Add(ab);
                }
                requiredAbilityRepository.Add(new RequiredAbility
                {
                    RequirementId = requirementRepository.GetNewestRequirement().RequirementId,
                    AbilityId = model.SelectedSalaryAbilitiyId
                });
                requiredAbilityRepository.Add(new RequiredAbility
                {
                    RequirementId = requirementRepository.GetNewestRequirement().RequirementId,
                    AbilityId = model.SelectedWorkedTimeAbilitiyId
                });
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
            return RedirectToAction("RequirementList", "Admin");
        }
        [HttpGet]
        public IActionResult EditRequirement(int id)
        {
            var requirement = requirementRepository.GetRequirement(id);
            RequirementViewModel model = new RequirementViewModel()
            {
                Requirement = requirement
            };
            var applyPositions = applyPositionRepository.GetApplyPositions();
            var applyPositionsAbilities = applyPositionAbilityRepository.GetApplyPositionAbilities(requirement.ApplyPositionId).Select(apa => apa.Ability);
            model.ApplyPositionAbilities = new List<RequirementViewModel.Ability>();
            foreach (var ability in applyPositionsAbilities)
            {
                var ab = new RequirementViewModel.Ability
                {
                    AbilityID = ability.AbilityId,
                    AbilityName = ability.AbilityName,
                    IsSelected = requiredAbilityRepository.GetRequiredAbilities(requirement.RequirementId).Where(ra => ra.AbilityId == ability.AbilityId).Any()
                };
                if (requiredAbilityRepository.GetRequiredAbilities(id).Where(ra => ra.AbilityId == ability.AbilityId).Any())
                    ab.IsSelected = true;
                else
                    ab.IsSelected = false;
                model.ApplyPositionAbilities.Add(ab);
            }
            model.SalaryAbilities = abilityRepository.GetAbilities().Where(a => a.Note == "Salary").ToList();
            model.SelectedSalaryAbilitiyId = requiredAbilityRepository.GetRequiredAbilities(requirement.RequirementId).FirstOrDefault(ra => ra.Ability.Note == "Salary").AbilityId;
            model.WorkedTimeAbilities = abilityRepository.GetAbilities().Where(a => a.Note == "WorkedTime").ToList();
            model.SelectedWorkedTimeAbilitiyId = requiredAbilityRepository.GetRequiredAbilities(requirement.RequirementId).FirstOrDefault(ra => ra.Ability.Note == "WorkedTime").AbilityId;
            return View(model);
        }
        [HttpPost]
        public IActionResult EditRequirement(RequirementViewModel model)
        {
            try
            {
                foreach (var ra in requiredAbilityRepository.GetRequiredAbilities(model.Requirement.RequirementId))
                {
                    requiredAbilityRepository.Delete(ra);
                }
                new CandidateManagementContext().SaveChanges();
                var selectedAbilities = model.ApplyPositionAbilities.Where(apa => apa.IsSelected).Select(a => a.AbilityID).ToList();
                selectedAbilities.Add(model.SelectedSalaryAbilitiyId);
                selectedAbilities.Add(model.SelectedWorkedTimeAbilitiyId);
                foreach (var abilityId in selectedAbilities)
                {
                    var ra = new RequiredAbility
                    {
                        RequirementId = model.Requirement.RequirementId,
                        AbilityId = abilityId
                    };
                    requiredAbilityRepository.Add(ra);

                }
                requirementRepository.Update(model.Requirement);
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
            return RedirectToAction("RequirementList", "Admin");
        }
        [HttpPost]
        public IActionResult DeleteRequirement(int id)
        {
            var requirement = requirementRepository.GetRequirement(id);
            requirementRepository.Delete(requirement);
            return Redirect(Request.Headers["Referer"].ToString());
        }
        [HttpGet]
        public IActionResult GetAbilityByApplyPosition(int applyPositionId)
        {
            RequirementViewModel model = new RequirementViewModel();
            var applyPositions = applyPositionRepository.GetApplyPositions();
            var applyPositionsAbilities = applyPositionAbilityRepository.GetApplyPositionAbilities(applyPositionId).Select(apa => apa.Ability);
            model.ApplyPositionAbilities = new List<RequirementViewModel.Ability>();
            foreach (var op in applyPositionsAbilities)
            {
                var o = new RequirementViewModel.Ability
                {
                    AbilityID = op.AbilityId,
                    AbilityName = op.AbilityName,
                    IsSelected = false
                };
                model.ApplyPositionAbilities.Add(o);
            }
            return PartialView("_ApplyPositionAbilityCheckBoxPartial", model);
        }

        #endregion

        #region Apply Detail
        [HttpPost]
        public IActionResult UpdateCVStatus(int id, string status)
        {
            if (status.Equals("pass"))
            {
                applyDetailRepository.UpdateCVStatus(id, "pass");
            }
            else if (status.Equals("fail"))
            {
                applyDetailRepository.UpdateCVStatus(id, "fail");
            }
            else
            {
                applyDetailRepository.UpdateCVStatus(id, "null");
            }
            return Redirect(Request.Headers["Referer"].ToString());
        }
        [HttpGet]
        public IActionResult ApplyDetailList()
        {
            ApplyDetailListViewModel model = new ApplyDetailListViewModel()
            {
                ApplyDetails = applyDetailRepository.GetApplyDetails()
            };
            return View(model);
        }
        #endregion

        #region Interview Schedule
        [HttpGet]
        public IActionResult InterviewScheduleList()
        {
            InterviewScheduleListViewModel model = new InterviewScheduleListViewModel()
            {
                InterviewSchedules = interviewScheduleRepository.GetInterviewSchedules()
            };
            return View(model);
        }
        [HttpGet]
        public IActionResult MarkAbility(int applyDetailId, int interviewId)
        {
            var applyDetail = applyDetailRepository.GetApplyDetail(applyDetailId);
            EvaluateViewModel model = new EvaluateViewModel()
            {
                ApplyDetail = applyDetail,
                InterviewSchedule = interviewScheduleRepository.GetInterviewSchedule(interviewId)
            };

            var interviewResults = interviewResultRepository.GetInterviewResultByInterviewID(interviewId);
            var signedInOperator = operatorRepository.GetOperator(userManager.GetUserId(User));
            if (signedInOperator != null)
            {
                var irOfOperator = interviewResultRepository.GetInterviewResultByInterviewID(interviewId).Where(ir => ir.OperatorId == signedInOperator.OperatorId);
                if (irOfOperator.Any())
                {
                    model.Operator = signedInOperator;
                    model.CanEvaluate = true;
                }
            }
            model.InterviewResult = interviewResultRepository.GetInterviewResultByInterviewIDAndOperatorID(interviewId, signedInOperator.OperatorId);

            var applyDetailAbilities = applyDetailAbilityRepository.GetApplyDetailAbility(applyDetailId);

            model.ApplyDetailAbilities = new List<EvaluateViewModel.ApplyDetailAbility>();
            foreach (var applyDetailAbility in applyDetailAbilities)
            {
                var ability = new EvaluateViewModel.ApplyDetailAbility
                {
                    ApplyDetailAbilityID = applyDetailAbility.ApplyDetailAbilityId,
                    AbilityName = applyDetailAbility.Ability.AbilityName,
                    IsSelected = false
                };
                if (signedInOperator != null)
                {
                    var interviewResult = model.InterviewResult.FirstOrDefault(ir => ir.ApplyDetailAbilityId == applyDetailAbility.ApplyDetailAbilityId && ir.OperatorId == signedInOperator.OperatorId && ir.Point > 0);
                    if (interviewResult != null)
                    {
                        ability.IsSelected = true;
                        ability.Point = interviewResult.Point;
                    }
                }


                model.ApplyDetailAbilities.Add(ability);
            }
            return PartialView("_MarkAbilityPartial", model);
        }
        [HttpPost]
        public IActionResult MarkAbility(EvaluateViewModel model)
        {
            try
            {
                foreach (var ability in model.ApplyDetailAbilities)
                {
                    var interviewResult = new InterviewResult
                    {
                        InterviewId = model.InterviewSchedule.InterviewId,
                        OperatorId = model.Operator.OperatorId,
                        ApplyDetailAbilityId = ability.ApplyDetailAbilityID,
                        Point = ability.IsSelected ? ability.Point : 0
                    };

                    interviewResultRepository.Update(interviewResult);
                }
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
            return Redirect(Request.Headers["Referer"].ToString());
        }
        [HttpPost]
        public IActionResult UpdateInterviewStatus(int id, string status)
        {
            if (status.Equals("pass"))
            {
                applyDetailRepository.UpdateInterviewStatus(id, "pass");
            }
            else if (status.Equals("fail"))
            {
                applyDetailRepository.UpdateInterviewStatus(id, "fail");
            }
            else
            {
                applyDetailRepository.UpdateInterviewStatus(id, "null");
            }
            return Redirect(Request.Headers["Referer"].ToString());
        }
        #endregion
    }
}
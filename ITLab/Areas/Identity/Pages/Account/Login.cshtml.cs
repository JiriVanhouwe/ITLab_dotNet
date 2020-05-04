using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using ITLab.Models;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;
using System.Text;

namespace ITLab.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private IUserRepository _userRepo;

        public LoginModel(SignInManager<IdentityUser> signInManager, 
            ILogger<LoginModel> logger,
            UserManager<IdentityUser> userManager,
            IUserRepository userRepo)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _userRepo = userRepo;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                
                //Because we have our own password system (with md5 encryption), we can't use this method. Therefor we made our own
                //var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);
                var result = await CustomSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    return LocalRedirect(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        public virtual Task<SignInResult> CustomSignInAsync(string email, string password, bool rememberMe, bool lockoutOnFailure)
        {
            return Task<SignInResult>.Run(async () =>
            {
                List<ItlabUser> itlabUsers = _userRepo.GetAllUsers();
                //Firstly check if there exists a user with the given email in our own DB as a ItlabUser
                if (itlabUsers.Any(user => user.Username.Equals(email)))
                {
                    //Convert the password to hash using md5, we will use the Microsoft Cryptography library
                    MD5 md5Hash = MD5.Create();
                    // Convert the input string to a byte array and compute the hash.
                    byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                    // Create a new Stringbuilder to collect the bytes and create a string.
                    StringBuilder sBuilder = new StringBuilder();

                    // Loop through each byte of the hashed data  and format each one as a hexadecimal string.
                    for (int i = 0; i < data.Length; i++)
                    {
                        sBuilder.Append(data[i].ToString("x2"));
                    }

                    string hashedPassword = sBuilder.ToString();
                    Console.WriteLine(hashedPassword);

                    //Check if the combination of email and password exists
                    foreach(ItlabUser user in itlabUsers)
                    {
                        if (user.Username.Equals(email) && user.Password.Equals(hashedPassword))
                        {
                            //If the combination is valid we can create an identityuser object and login the user

                            //Firstly we check if there's already a user object with the given email in the AspNetUsers table
                            IdentityUser identityUser = _userManager.FindByIdAsync(email).Result;
                            if (identityUser == null)
                            {
                                //If that isn't the case we create a new one that gets saved in the AspNetUsers table
                                identityUser = new IdentityUser(email) { Id = email, PasswordHash = hashedPassword };
                                await _userManager.CreateAsync(identityUser);
                                await _userManager.SetUserNameAsync(identityUser, user.Firstname + "" + user.Lastname);

                                //We then set the role based on the usertype
                                string userRole = user.UserType.ToString();
                                await _userManager.AddToRoleAsync(identityUser, userRole);
                            }
                            else
                            {
                                //When there's already an entry for this user we should check if their role has changed
                                var roles = await _userManager.GetRolesAsync(identityUser);
                                if (!roles.Contains(user.UserType.ToString()))
                                {
                                    //We remove all the previous roles from the user
                                    await _userManager.RemoveFromRolesAsync(identityUser, roles);
                                    //Then we add the right one
                                    string userRole = user.UserType.ToString();
                                    await _userManager.AddToRoleAsync(identityUser, userRole);
                                }
                            }

                            //Finally we can perform the actual login and tell the userRepo which user is logged in
                            await _signInManager.SignInAsync(identityUser, isPersistent: false);
                            IUserRepository.LoggedInUser = user;
                            Console.WriteLine("Great succes");
                            return SignInResult.Success;
                        }
                    }
                }
                //If not the user could be in the AspNetUser DB
                else
                {
                    await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);
                }
                return SignInResult.Failed;
            });
        }
    }
}

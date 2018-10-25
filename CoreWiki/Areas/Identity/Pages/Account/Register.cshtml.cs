using CoreWiki.Areas.Identity.Services;
using CoreWiki.Data.EntityFramework.Security;
using CoreWiki.Notifications.Abstractions.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace CoreWiki.Areas.Identity.Pages.Account
{
	[AllowAnonymous]
	public class RegisterModel : PageModel
	{
		private readonly SignInManager<CoreWikiUser> _signInManager;
		private readonly UserManager<CoreWikiUser> _userManager;
		private readonly ILogger<RegisterModel> _logger;
		private readonly IEmailSender _emailSender;
		private readonly INotificationService _notificationService;
		private readonly HIBPClient _HIBPClient;

		public RegisterModel(
			UserManager<CoreWikiUser> userManager,
			SignInManager<CoreWikiUser> signInManager,
			ILogger<RegisterModel> logger,
			IEmailSender emailSender,
			INotificationService notificationService,
			HIBPClient hIBPClient)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_logger = logger;
			_emailSender = emailSender;
			_notificationService = notificationService;
			_HIBPClient = hIBPClient;
		}

		[BindProperty]
		public InputModel Input { get; set; }

		public string ReturnUrl { get; set; }

		public class InputModel
		{
			[Required]
			[EmailAddress]
			[Display(Name = "Email")]
			public string Email { get; set; }

			[Required]
			[StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
			[DataType(DataType.Password)]
			[Display(Name = "Password")]
			public string Password { get; set; }

			[DataType(DataType.Password)]
			[Display(Name = "Confirm password")]
			[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
			public string ConfirmPassword { get; set; }

			[Display(Name = "Opt-in to notifications?")]
			public bool CanNotify { get; set; }
		}

		public void OnGet(string returnUrl = null)
		{
			ReturnUrl = returnUrl;
		}

		public async Task<IActionResult> OnPostAsync(string returnUrl = null)
		{
			returnUrl = returnUrl ?? Url.Content("~/");

			if (!ModelState.IsValid)
			{
				return Page();
			}

			var passwordCheck = await _HIBPClient.GetHitsPlainAsync(Input.Password);
			if (passwordCheck > 0)
			{
				ModelState.AddModelError(nameof(Input.Password), "This password is known to hackers, and can lead to your account being compromised, please try another password. For more info goto https://haveibeenpwned.com/Passwords");
				return Page();
			}

			var user = new CoreWikiUser
			{
				UserName = Input.Email,
				Email = Input.Email,
				CanNotify = Input.CanNotify
			};

			var result = await _userManager.CreateAsync(user, Input.Password);
			if (result.Succeeded)
			{
				_logger.LogInformation("User created a new account with password.");

				var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
				await _notificationService.SendConfirmationEmail(Input.Email, user.Id, code);

				await _signInManager.SignInAsync(user, isPersistent: false);
				return LocalRedirect(returnUrl);
			}
			foreach (var error in result.Errors)
			{
				ModelState.AddModelError(string.Empty, error.Description);
			}

			// If we got this far, something failed, redisplay form
			return Page();
		}
	}
}

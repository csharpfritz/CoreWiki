using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using CoreWiki.Data.EntityFramework.Security;
using CoreWiki.Core.Notifications;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoreWiki.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ForgotPasswordModel : PageModel
    {
        private readonly UserManager<CoreWikiUser> _userManager;
        private readonly IEmailSender _emailSender;
	    private readonly INotificationService _notificationService;

	    public ForgotPasswordModel(UserManager<CoreWikiUser> userManager, IEmailSender emailSender, INotificationService notificationService)
        {
            _userManager = userManager;
            _emailSender = emailSender;
	        _notificationService = notificationService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(Input.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return RedirectToPage("./ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please
                // visit https://go.microsoft.com/fwlink/?LinkID=532713
                var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
	            await _notificationService.SendForgotPasswordEmail(Input.Email, resetToken, () => user.CanNotify);

                return RedirectToPage("./ForgotPasswordConfirmation");
            }

            return Page();
        }
    }
}

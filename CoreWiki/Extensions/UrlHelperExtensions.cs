﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWiki.Extensions
{
	public static class UrlHelperExtensions
	{
	  public static string GetLocalUrl(this IUrlHelper urlHelper, string localUrl)
	  {
		if (!urlHelper.IsLocalUrl(localUrl))
		{
		  return urlHelper.Page("/Details");
		}

		return localUrl;
	  }

	  public static string EmailConfirmationLink(this IUrlHelper urlHelper, string userId, string code, string scheme)
	  {
		return urlHelper.Page(
			"/Account/ConfirmEmail",
			pageHandler: null,
			values: new { userId, code },
			protocol: scheme);
	  }

	  public static string ResetPasswordCallbackLink(this IUrlHelper urlHelper, string userId, string code, string scheme)
	  {
		return urlHelper.Page(
			"/Account/ResetPassword",
			pageHandler: null,
			values: new { userId, code },
			protocol: scheme);
	  }
	}
}

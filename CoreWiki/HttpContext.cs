using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CoreWiki
{
  namespace WikiHttpContext
  {
	public static class HttpContext
	{
	  private static IHttpContextAccessor _contextAccessor;

	  public static Microsoft.AspNetCore.Http.HttpContext Current => _contextAccessor.HttpContext;

	  internal static void Configure(IHttpContextAccessor contextAccessor)
	  {
		_contextAccessor = contextAccessor;
	  }
	}
  }
}

using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWiki.Configuration.Startup
{
    public static class ConfigureMediator
    {
		public static IServiceCollection AddMediator(this IServiceCollection services)
		{
			services.AddMediatR();
			return services;
		}
	}
}

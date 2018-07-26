using Microsoft.Extensions.DependencyInjection;

namespace CoreWiki.Extensibility.Common
{
    public static class StartupExtensions
    {
        public static IServiceCollection ConfigureExtensibility(this IServiceCollection services)
        {
            var moduleEvents = new CoreWikiModuleEvents();

            services.AddSingleton<ICoreWikiModuleHost, CoreWikiModuleHost>();
            services.AddSingleton<IExtensibilityManager, ExtensibilityManager>();
            services.AddSingleton<ICoreWikiModuleEvents>(moduleEvents);
            services.AddSingleton<ICoreWikiModuleLoader, CoreWikiModuleLoader>();

            return services;
        }
    }
}


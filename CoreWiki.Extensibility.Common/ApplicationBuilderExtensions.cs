using Microsoft.AspNetCore.Builder;

namespace CoreWiki.Extensibility.Common
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseExtensibility(this IApplicationBuilder applicationBuilder)
        {
            // Hack to create the ExtensibilityManager on application startup instead of first use
            // this will load all extensibitity modules on startup
            var extensibilityManager = applicationBuilder.ApplicationServices.GetService(typeof(IExtensibilityManager));

            return applicationBuilder;
        }
    }
}

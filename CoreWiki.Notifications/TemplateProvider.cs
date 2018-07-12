using CoreWiki.Core.Notifications;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using System;

namespace CoreWiki.Notifications
{
    public class TemplateProvider : ITemplateProvider
    {
        public const string NewCommentEmail = "NewCommentEmail";
        private const string TemplateRoot = "EmailTemplates";
        private const string TemplateExtension = ".cshtml";
        private readonly IRazorViewEngine _viewEngine;

        public TemplateProvider(IRazorViewEngine viewEngine)
        {
            _viewEngine = viewEngine;
        }

        public IView GetTemplate(string templateName)
        {
            var viewPath = $"/{TemplateRoot}/{templateName}{TemplateExtension}";
            var viewEngineResult = _viewEngine.GetView(executingFilePath: null, viewPath: viewPath, isMainPage: true);
            if (viewEngineResult.Success) return viewEngineResult.View;

            var searchedLocations = string.Concat(viewEngineResult.SearchedLocations);

            throw new Exception($"Template: '{templateName}' not found. Searched locations: {searchedLocations}");
        }
    }
}

using System;
using System.IO;
using System.Threading.Tasks;

namespace CoreWiki.Services
{
	public class TemplateProvider : ITemplateProvider
    {
		public const string DEFAULT_TEMPLATE_EXTENSION = "tmpl";
		private readonly string _templateRootFolder;
		private readonly string _templateExtension;

		public TemplateProvider(string templateRootFolder, string templateExtension = DEFAULT_TEMPLATE_EXTENSION)
		{
			_templateRootFolder = templateRootFolder;
			_templateExtension = templateExtension;
		}

		public async Task<string> GetTemplateContent(string templateName)
		{
			var fullPath = GetFullPath(_templateRootFolder, $"{templateName}.{_templateExtension}");

			if (!File.Exists(fullPath))
			{
				throw new Exception($"Template {templateName} was not found in {_templateRootFolder}");
			}

			return await File.ReadAllTextAsync(fullPath);
		}

		private string GetFullPath(string path, string filename)
		{
			return Path.Combine(path, filename);
		}
	}
}

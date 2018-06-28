using System.Threading.Tasks;

namespace CoreWiki.Services
{
	public class EmailMessageFormatter : IEmailMessageFormatter
	{
		private readonly ITemplateProvider _templateProvider;
		private readonly ITemplateParser _templateParser;

		public EmailMessageFormatter(ITemplateProvider templateProvider, ITemplateParser templateParser)
		{
			_templateProvider = templateProvider;
			_templateParser = templateParser;
		}
		public async Task<string> FormatEmailMessage<T>(string templateName, T model) where T : class
		{
			var template = await _templateProvider.GetTemplateContent(templateName);

			return _templateParser.Format<T>(template, model);
		}
	}
}

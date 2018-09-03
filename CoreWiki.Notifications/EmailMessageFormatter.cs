using System.Threading.Tasks;
using CoreWiki.Notifications.Abstractions.Notifications;

namespace CoreWiki.Notifications
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

		public async Task<string> FormatEmailMessage<TModel>(string templateName, TModel model) where TModel : class
		{
			var template = _templateProvider.GetTemplate(templateName);

			return await _templateParser.Parse(template, model);
		}
	}
}

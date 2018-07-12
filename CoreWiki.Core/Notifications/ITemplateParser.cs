namespace CoreWiki.Core.Notifications
{
	public interface ITemplateParser
    {
		string Format<T>(string template, T model) where T : class;
    }
}

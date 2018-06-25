namespace CoreWiki.Services
{
	public interface ITemplateParser
    {
		string Format<T>(string template, T model) where T : class;
    }
}

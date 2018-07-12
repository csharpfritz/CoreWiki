using CoreWiki.Core.Notifications;
using System.Collections.Generic;
using System.Reflection;

namespace CoreWiki.Notifications
{
	public class TemplateParser : ITemplateParser
	{
		public TemplateParser()
		{
		}

		public string Format<T>(string template, T model) where T : class
		{
			var propertyValueDict = GetPropertyValueDictionary<T>(model);

			foreach (var propertyValue in propertyValueDict)
			{
				var placeholder = $"{{{{{propertyValue.Key}}}}}";
				template = template.Replace(placeholder, propertyValue.Value.ToString());
			}

			return template;
		}

		private IDictionary<string, object> GetPropertyValueDictionary<T>(T model) where T: class
		{
			var result = new Dictionary<string, object>();

			foreach (var property in model.GetType().GetProperties())
			{
				result.Add(property.Name, property.GetValue(model, null));
			}

			return result;
		}
	}
}

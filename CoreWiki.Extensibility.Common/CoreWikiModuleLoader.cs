using McMaster.NETCore.Plugins;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace CoreWiki.Extensibility.Common
{
	public class CoreWikiModuleLoader : ICoreWikiModuleLoader
	{
		private const string ModuleFilter = "*.dll";

		public CoreWikiModuleLoader()
		{
		}

		public List<ICoreWikiModule> Load(params string[] paths)
		{
			var result = new List<ICoreWikiModule>();

			foreach (var path in paths)
			{
				var modules = Load(path);
				result.AddRange(modules);
			}

			return result;
		}

		public List<ICoreWikiModule> Load(string path)
		{
			var result = new List<ICoreWikiModule>();
			var files = GetFiles(path);
			var moduleType = typeof(ICoreWikiModule);
			var sharedTypes = new[] { typeof(ICoreWikiModule), typeof(ICoreWikiModuleHost), typeof(ILoggerFactory) };

			foreach (var file in files)
			{

				//var loader = PluginLoader.CreateFromAssemblyFile(file, sharedTypes);
				//var thatAssembly = loader.LoadDefaultAssembly();
				//var moduleTypes = thatAssembly.GetTypes().Where(t => t.GetInterfaces().Contains(moduleType)).ToArray();

				//foreach (var module in moduleTypes)
				//{
				//	var instance = Activator.CreateInstance(module) as ICoreWikiModule;
				//	result.Add(instance);
				//}

				var assembly = Assembly.LoadFile(file);
				var assemblyTypes = assembly.GetTypes().Where(t => t.GetInterfaces().Contains(moduleType)).ToArray();

				foreach (var assemblyType in assemblyTypes)
				{
					InitializeCoreWikiModule(assemblyType, assembly, result);
				}
			}

			return result;
		}

		private void InitializeCoreWikiModule(Type type, Assembly assembly, List<ICoreWikiModule> result)
		{
			if (!type.GetInterfaces().Contains(typeof(ICoreWikiModule))) return;

			var assemblyQualifiedTypeName = $"{type.FullName},{assembly.FullName}";
			var instance = Activator.CreateInstance(Type.GetType(assemblyQualifiedTypeName));
			if (instance is ICoreWikiModule module)
			{
				result.Add(module);
			}
		}

		private IEnumerable<string> GetFiles(string path, string filter = ModuleFilter)
		{
			if (!Directory.Exists(path)) return new string[0];
			return Directory.GetFiles(path, filter);
		}
	}
}

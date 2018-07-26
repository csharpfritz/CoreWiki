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

            foreach (var file in files)
            {
                var assembly = Assembly.LoadFile(file);
                var assemblyTypes = assembly.GetTypes();

                foreach (var assemblyType in assemblyTypes)
                {
                    InitializeCoreWikiModule(assemblyType, assembly, result);
                }
            }

            return result;
        }

        private void InitializeCoreWikiModule(Type type, Assembly assembly, List<ICoreWikiModule> result)
        {
            if(!type.GetInterfaces().Contains(typeof(ICoreWikiModule))) return;

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

using System.Collections.Generic;

namespace CoreWiki.Extensibility.Common.Events
{
    public class CoreWikiModuleValidationEventArgs : CoreWikiModuleCancelEventArgs
    {
        public List<ValidationResult> ValidationResults { get; } = new List<ValidationResult>();
    }
}

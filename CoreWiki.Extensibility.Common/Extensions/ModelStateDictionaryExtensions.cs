using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

namespace CoreWiki.Extensibility.Common.Extensions
{
    public static class ModelStateDictionaryExtensions
    {
        /// <summary>
        /// Binds the validation results from CoreWikiModuleValidationEventArgs to the ModelStateDictionary.
        /// </summary>
        /// <param name="modelStateDictionary">The model state dictionary.</param>
        /// <param name="validationResults">The validation results to bind to the model state dictionary.</param>
        public static void BindValidationResult(this ModelStateDictionary modelStateDictionary, IEnumerable<ValidationResult> validationResults)
        {
            if (validationResults == null) return;
            if (!validationResults.Any()) return;

            foreach (var validationResult in validationResults)
            {
                modelStateDictionary.AddModelError(validationResult.ErrorProperty, validationResult.ErrorMessage);
            }
        }
    }
}

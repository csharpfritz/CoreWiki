using CoreWiki.Extensibility.Common.Events;
using System.Linq;

namespace CoreWiki.Extensibility.Common.Extensions
{
    public static class CoreWikiModuleValidationEventArgsExtensions
    {
        /// <summary>
        /// Get a value indicating wheter there are any validation errors.
        /// </summary>
        /// <param name="eventArgs"></param>
        /// <returns>A value indicating wheter there are any validation errors.</returns>
        public static bool HasValidationErrors(this CoreWikiModuleValidationEventArgs eventArgs)
        {
            return eventArgs.ValidationResults.Any();
        }

        /// <summary>
        /// Adds a validation error the the validation errors collection.
        /// </summary>
        /// <param name="eventArgs"></param>
        /// <param name="errorProperty">The name of the property for which to add a validation error.</param>
        /// <param name="errorMessage">The error message of the validation error.</param>
        public static void AddValidationError(this CoreWikiModuleValidationEventArgs eventArgs, string errorProperty, string errorMessage = "")
        {
            eventArgs.ValidationResults.Add(
                new ValidationResult
                {
                    ErrorProperty = errorProperty,
                    ErrorMessage = errorMessage,
                });
        }
    }
}

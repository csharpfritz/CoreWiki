namespace CoreWiki.Extensibility.Common
{
    public class ValidationResult
    {
        /// <summary>
        /// The name of the property that has an error.
        /// </summary>
        public string ErrorProperty { get; set; } = string.Empty;

        /// <summary>
        /// The error message for the property.
        /// </summary>
        public string ErrorMessage { get; set; } = string.Empty;
    }
}
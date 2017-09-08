using System.Globalization;
using System.Windows.Controls;

namespace SCDownloader.Common
{
    internal class DirectoryFormatValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value is string path && Utilities.IsPathValidRootedLocal(path))
            {
                return ValidationResult.ValidResult;
            }
            return new ValidationResult(false, "Directory must be a valid format");
        }
    }
}

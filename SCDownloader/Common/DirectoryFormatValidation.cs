using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using SCDownloader.Models;

//https://stackoverflow.com/a/30910150
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

        public override ValidationResult Validate(object value, CultureInfo cultureInfo, BindingExpressionBase owner)
        {
            ValidationResult result = base.Validate(value, cultureInfo, owner);
            var vm = (MainWindowVM) ((BindingExpression) owner).DataItem;
            vm.IsReadyToDownload = result.IsValid;
            return result;
        }
    }
}

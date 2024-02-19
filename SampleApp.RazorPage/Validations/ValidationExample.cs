using System.ComponentModel.DataAnnotations;

namespace SampleApp.RazorPage.Validations
{
    public class ExampleValidationAttribute : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            var code = value as string;
            if (code == null)
            {
                return new ValidationResult("Not a valid code");
            }
            return ValidationResult.Success;
        }
    }
}

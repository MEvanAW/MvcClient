using System.ComponentModel.DataAnnotations;

namespace MvcClient.Attribute
{
    public class StockAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;

        public StockAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null)
            {
                throw new ArgumentException(nameof(value) + " is null.");
            }
            ErrorMessage = ErrorMessageString;
            var currentValue = (int) value;
            var property = validationContext.ObjectType.GetProperty(_comparisonProperty);
            if (property is null)
            {
                throw new ArgumentException("Property with this name is not found.");
            }
            var objectValue = property.GetValue(validationContext.ObjectInstance); 
            if (objectValue is null)
            {
                throw new ArgumentException("Property is null");
            }
            var comparisonValue = (int) objectValue;
            if (currentValue > comparisonValue)
                return new ValidationResult(ErrorMessage);

            return ValidationResult.Success;
        }
    }
}

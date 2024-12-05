
namespace Business.Exceptions
{
    public class ValidationException : Exception
    {
        public IDictionary<string, string> ValidationErrors { get; private set; }

        public ValidationException()
        {
            ValidationErrors = new Dictionary<string, string>();
        }

        public ValidationException(string message) : base(message)
        {
            ValidationErrors = new Dictionary<string, string>();
        }

        public void AddValidationError(string field, string errorMessage)
        {
            if (!ValidationErrors.ContainsKey(field))
            {
                ValidationErrors.Add(field, errorMessage);
            }
        }

        public override string ToString()
        {
            string errorMessages = string.Join("; ", ValidationErrors);
            return $"Validation failed: {errorMessages}";
        }
    }
}

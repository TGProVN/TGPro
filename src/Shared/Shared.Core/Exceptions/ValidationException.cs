using FluentValidation.Results;
using Shared.Core.Constants;
using Shared.Core.Wrapper.Errors;

namespace Shared.Core.Exceptions;

public sealed class ValidationException : Exception
{
    private readonly IList<ValidationError> _errors;

    private ValidationException() : base(AppConstants.Messages.ValidationError)
    {
        _errors = new List<ValidationError>();
    }

    public ValidationException(IEnumerable<ValidationFailure> failures) : this()
    {
        foreach (var failure in failures)
        {
            _errors.Add(new ValidationError {
                Code = failure.ErrorCode,
                Message = failure.ErrorMessage,
                PropertyName = failure.PropertyName,
                StackTrace = StackTrace
            });
        }
    }

    public ValidationException(string errorCode, string message, string propertyName) : this()
    {
        _errors.Add(new ValidationError {
            Code = errorCode,
            Message = message,
            PropertyName = propertyName,
            StackTrace = StackTrace
        });
    }

    public IList<ValidationError> GetErrors()
    {
        return _errors;
    }

    public IList<string?> GetErrorMessages()
    {
        return _errors.Select(x => x.Message).ToList();
    }
}

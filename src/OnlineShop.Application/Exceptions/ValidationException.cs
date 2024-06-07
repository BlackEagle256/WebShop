using FluentValidation.Results;
using OnlineShop.Domain.Abstractions.Primitives;

namespace OnlineShop.Application.Exceptions;
/// <summary>
/// Represents an exception that occurs when a validation fails.
/// </summary>
public sealed class ValidationException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationException"/> class.
    /// </summary>
    /// <param name="failures">The collection of validation failures.</param>
    public ValidationException(IEnumerable<ValidationFailure> failures)
        : base("One or more validation failures has occurred.") =>
        Errors = failures
            .Distinct()
            .Select(failure => new Error(failure.ErrorCode, failure.ErrorMessage))
            .ToList();

    /// <summary>
    /// Gets the validation errors.
    /// </summary>
    public IReadOnlyCollection<Error> Errors { get; }
    /// <summary>
    /// Convert the FluentValidation.ValidationException to my custom ValidationException
    /// </summary>
    /// <param name="exception"></param>
    public static implicit operator ValidationException(FluentValidation.ValidationException exception) => 
        new ValidationException(exception.Errors);
}
﻿using Buyersoft.Application.Messaging;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Buyersoft.Application.Behavior;

public sealed class ValidationBehavior<TRequest, TResponse> :
   IPipelineBehavior<TRequest, TResponse>
   where TRequest : class, ICommand<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }

        var context = new ValidationContext<TRequest>(request);

        var errorDictionary = _validators
            .Select(x => x.Validate(context))
            .SelectMany(x => x.Errors)
            .Where(x => x != null)
            .GroupBy(
            x => x.PropertyName,
            x => x.ErrorMessage, (propertyName, errorMessage) => new
            {
                Key = propertyName,
                Values = errorMessage.Distinct().ToArray()
            })
            .ToDictionary(x => x.Key, x => x.Values[0]);

        if (errorDictionary.Any())
        {
            var errors = errorDictionary.Select(s => new ValidationFailure
            {
                PropertyName = s.Key,
                ErrorMessage = s.Value,
                ErrorCode = s.Key
            }).ToList();
            throw new ValidationException(errors);
        }

        return await next();
    }
}

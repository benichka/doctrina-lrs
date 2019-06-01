﻿using FluentValidation;

namespace Doctrina.xAPI.Validators
{
    public class ExtensionsValidator : AbstractValidator<Extensions>
    {
        public ExtensionsValidator()
        {
            RuleFor(x => x.Failures).Custom((x, context) =>
            {
                foreach (var failure in x)
                {
                    context.AddFailure(failure.Name, failure.Message);
                }
            });
        }
    }
}

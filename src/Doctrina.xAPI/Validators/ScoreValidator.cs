﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctrina.xAPI.Validators
{
    public class ScoreValidator : AbstractValidator<Score>
    {
        public ScoreValidator()
        {
            RuleFor(x => x.Raw).LessThanOrEqualTo(x => x.Min)
                .Unless(x=> x.Raw == null || x.Min == null);

            RuleFor(x => x.Max).GreaterThan(x => x.Min)
                .Unless(x=> x.Max == null || x.Min == null);

            RuleFor(x => x.Min).LessThan(x => x.Max)
                .Unless(x => x.Min == null || x.Max == null);

            RuleFor(x => x.Scaled).GreaterThanOrEqualTo(-1)
                .LessThanOrEqualTo(1)
                .Unless(x=> x.Scaled == null);
        }
    }
}

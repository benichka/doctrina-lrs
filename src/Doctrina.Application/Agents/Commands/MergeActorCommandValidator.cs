﻿using Doctrina.xAPI;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctrina.Application.Agents.Commands
{
    public class MergeActorCommandValidator : AbstractValidator<MergeActorCommand>
    {
        public MergeActorCommandValidator()
        {
            RuleFor(x => x).Must(actor => !actor.IsAnonymous() && actor.GetIdentifiers().Count > 1)
                .WithMessage($"An Identified Group/Agent does not allow for multiple identifiers.");

            RuleFor(x => x.ObjectType)
                .Must(x => x == xAPI.ObjectType.Agent || x == xAPI.ObjectType.Group)
                .WithMessage(x=> $"Cannot create Agent/Group with objectType '{x.ObjectType}'");

            //When(x => x.ObjectType == xAPI.ObjectType.Group, 
            //    RuleFor(x=> (Group)x))
            //    .Otherwise();
            //"An Anonymous Group MUST NOT contain Group Objects in the 'member' identifiers."

            RuleForEach(x => x.Member).NotEqual(x => x.ObjectType == ObjectType.Group
                .When;

            RuleFor(x=> x.ObjectType)
                .When
        }

        public static bool IsAnonymous()
        {

        }
    }
}

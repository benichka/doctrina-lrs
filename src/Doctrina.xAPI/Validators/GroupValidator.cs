﻿using FluentValidation;
using System.Linq;

namespace Doctrina.xAPI.Validators
{
    public class GroupValidator : AbstractValidator<Group>
    {
        public GroupValidator()
        {
            // Requirements for Groups
            RuleFor(x => x.ObjectType).Equal(ObjectType.Group);

            // Requirements for Anonymous Groups
            RuleFor(x => x.Member)
                .Must(x => x.Count() > 1)
                .Unless(IsIdentifiedGroup)
                .WithMessage("An Anonymous Group MUST include a \"member\" property listing constituent Agents.");

            RuleForEach(x => x.Member)
                .SetValidator(new AgentValidator())
                .Unless(IsIdentifiedGroup)
                .WithMessage("An Anonymous Group MUST NOT contain Group Objects in the \"member\" identifiers.");

            // Requirements for Identified Groups
            RuleFor(x => x).Must(MustHaveSingleIdentifier)
                .When(IsIdentifiedGroup)
                .WithMessage("An Identified Group MUST include exactly one (1) Inverse Functional Identifier.");

            RuleForEach(x => x.Member)
                .SetValidator(new AgentValidator())
                .When(IsIdentifiedGroup)
                .WithMessage("An Identified Group MUST NOT contain Group Objects in the \"member\" property.");
        }

        private bool MustHaveSingleIdentifier(Group arg)
        {
            int count = 0;

            if (arg.Mbox != null)
                count++;

            if (!string.IsNullOrEmpty(arg.MboxSHA1SUM))
                count++;

            if (arg.OpenId != null)
                count++;

            if (arg.Account != null)
            {
                if (!string.IsNullOrEmpty(arg.Account.Name) && arg.Account.HomePage != null)
                {
                    count++;
                }
            }

            return count == 1;
        }

        private bool IsIdentifiedGroup(Group arg)
        {
            if (arg.Mbox != null)
                return true;

            if (!string.IsNullOrEmpty(arg.MboxSHA1SUM))
                return true;

            if (arg.OpenId != null)
                return true;

            if (arg.Account != null)
            {
                if (!string.IsNullOrEmpty(arg.Account.Name) && arg.Account.HomePage != null)
                {
                    return true;
                }
            }

            return false;
        }
    }
}

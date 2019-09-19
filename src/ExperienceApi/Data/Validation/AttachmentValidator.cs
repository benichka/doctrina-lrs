﻿using FluentValidation;

namespace Doctrina.ExperienceApi.Data.Validation
{
    public class AttachmentValidator : AbstractValidator<Attachment>
    {
        public AttachmentValidator()
        {
            RuleFor(x => x.UsageType).NotEmpty();
            RuleFor(x => x.Display).NotEmpty().SetValidator(new LanguageMapValidator());
            RuleFor(x => x.Description).SetValidator(new LanguageMapValidator()).When(x => x.Description != null);
            RuleFor(x => x.ContentType).NotEmpty();
            RuleFor(x => x.Length).NotEmpty();
            RuleFor(x => x.SHA2).NotEmpty();

            // TODO: Create validation rules for the validation of Signatures
            //if (UsageType == new Iri("http://adlnet.gov/expapi/attachments/signature")
            //    && ContentType == "application/octet-stream")
            //{
            //    // Verify signatures are well formed

            //    // Decode the JWS signature, and load the signed serialization of the Statement from the JWS signature payload.

            //    // Validate that the original Statement is logically equivalent to the received Statement.

            //    // If the JWS header includes an X.509 certificate, validate the signature against that certificate as defined in JWS.

            //    // Validate that the signature requirements outlined above have been met.
            //}
        }
    }
}

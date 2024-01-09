﻿using Dictionary.Common.Models.RequestModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary.Api.Application.Features.Commands.User;

public class LoginUserCommandValidator:AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(x=>x.EmailAddress)
            .NotNull()
            .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible)
            .WithMessage("{PropertyName} not a valid email address");

        RuleFor(x => x.Password)
            .NotNull()
            .MinimumLength(6)
            .WithMessage("{PropertyName} should at least be {MinLenght} characters");
    }
}

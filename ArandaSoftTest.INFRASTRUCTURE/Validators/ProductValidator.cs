using ArandaSoftTest.CORE.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArandaSoftTest.INFRASTRUCTURE.Validators
{
    public class ProductValidator : AbstractValidator<ProductDto>
    {
        public ProductValidator()
        {
            RuleFor(product => product.Name)
                .NotNull()
                .Length(5, 200);

            RuleFor(product => product.Description)
                .NotNull()
                .Length(10, 500);
        }
    }
}

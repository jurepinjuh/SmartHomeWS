using FluentValidation;
using SmartHome.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHome.Validators
{
    public class SmartHomeDataValidator:AbstractValidator<SmartHomeData>
    {
        public SmartHomeDataValidator()
        {
            RuleFor(s => s.Humidity).NotNull();
            RuleFor(s => s.Temperature).NotNull();
        }
    }
}

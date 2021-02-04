using FluentValidation;
using SmartHome.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHome.Validators
{
    public class SettingsValidator:AbstractValidator<SmartHomeSettings>
    {
        public SettingsValidator()
        {
            RuleFor(s => s.Power).NotNull();
            RuleFor(s => s.Interval).NotEmpty().GreaterThan(0);
            RuleFor(s => s.WorkingFrom).NotNull().GreaterThan(-1);
            RuleFor(s => s.WorkingTo).NotNull().GreaterThan(-1);
            RuleFor(s => s.WorkingFrom).LessThanOrEqualTo(s => s.WorkingTo);
        }
    }
}

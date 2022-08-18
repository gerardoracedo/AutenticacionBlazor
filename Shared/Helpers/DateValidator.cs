using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Runtime;
using System.Collections;

namespace AutenticacionBlazor.Shared.Helpers
{
    class DateValidator : ValidationAttribute
    {
        public DateValidator() { }

        public string GetErrorMessage() => "La fecha no puede ser mayor a la de hoy.";
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var date = (DateTime)value;

            if (DateTime.Compare(date, DateTime.Now) > 0) return new ValidationResult(GetErrorMessage());
            else return ValidationResult.Success;
        }
    }
}






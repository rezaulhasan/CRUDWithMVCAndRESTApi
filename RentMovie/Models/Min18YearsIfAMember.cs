using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RentMovie.Models
{
    public class Min18YearsIfAMember : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var customer = (Customer)validationContext.ObjectInstance;

            if (customer.MembershipTypeId == MembershipType.UnKnown ||customer.MembershipTypeId == MembershipType.PayAsYouGo)
                return ValidationResult.Success;

            if (customer.Birthdate == null)
                return new ValidationResult("Birth day can not be null");

            var age = DateTime.Now.Year - customer.Birthdate.Value.Year;

            return (age >= 18) 
                ? ValidationResult.Success
                : new ValidationResult("Customer's need to be 18 for membership");   
        }
    }
}
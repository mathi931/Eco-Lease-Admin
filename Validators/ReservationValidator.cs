using EcoLease_Admin.Data;
using EcoLease_Admin.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoLease_Admin.Validators
{
    public class ReservationValidator : AbstractValidator<Reservation>
    {
        public ReservationValidator()
        {
            RuleFor(r => r.LeaseBegin)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{PropertyName} is Empty")
                .Must(BeAValidStartDate).WithMessage("{PropertyName} cannot be older than 6 years and can not start later than a year from now");

            RuleFor(r => r.LeaseLast)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{PropertyName} is Empty")
                .Must(BeAValidEndDate).WithMessage("{PropertyName} cannot be older than 6 years and can not last from now more than 6 years from now");

            RuleFor(v => v.Status)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{PropertyName} is Empty")
                .Must(BeAValidStatus).WithMessage("{PropertyName} must be on Pending or Active or Declined or Expired");

        }
        protected bool BeAValidStatus(string status)
        {
            //it the status are one of these return true else false
            return status == "Pending" || status == "Active" || status == "Declined" || status == "Expired";
        }

        protected bool BeAValidStartDate(DateTime date)
        {
            int currentYear = DateTime.Now.Year;

            //returns true if the input year is maximal a year ahead and not older than 6 years
            return date.Year <= (currentYear + 1) && date.Year >= currentYear - 6;
        }

        protected bool BeAValidEndDate(DateTime date)
        {
            int currentYear = DateTime.Now.Year;

            //returns true if the input year is not older than 6 years(because of update) and not in the future with 6 years(5 years max)
            return date.Year <= (currentYear + 6) && date.Year >= (currentYear - 6);
        }
    }
}
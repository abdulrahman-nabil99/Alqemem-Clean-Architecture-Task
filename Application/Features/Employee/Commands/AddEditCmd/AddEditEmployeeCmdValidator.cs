using FluentValidation;

namespace CleanArchTask.Application.Features.Employee.Commands.AddEditCmd
{
    public class AddEditEmployeeCmdValidator:AbstractValidator<AddEditEmployeeCmd>
    {
        public AddEditEmployeeCmdValidator() 
        {
            RuleFor(e=>e.Id)
                .Must(id => id == null || id >= 0).WithMessage("Id Can't be Less Than 0");
            RuleFor(e => e.FNameAr)
                .NotEmpty().WithMessage("You must provide The Arabic First Name");
            RuleFor(e => e.LNameAr)
                .NotEmpty().WithMessage("You must provide The Arabic Last Name");
            RuleFor(e => e.FNameEn)
                .NotEmpty().WithMessage("You must provide The English First Name");
            RuleFor(e => e.LNameEn)
                .NotEmpty().WithMessage("You must provide The English Last Name");
            RuleFor(e => e.Email)
                .NotEmpty().EmailAddress().WithMessage("Invalid Email");
            RuleFor(e => e.Address)
                .NotEmpty().WithMessage("You must provide the address");
            RuleFor(e => e.Mobile)
                .NotEmpty().WithMessage("You must provide the Mobile")
                .Matches(@"^05\d{5,13}$").WithMessage("Mobile number must start with '05' and be between 7 and 15 characters.");
            RuleFor(e => e.Age)
                .NotNull().WithMessage("You must provide Age of the Employee")
                .Must(age=>age>0).WithMessage("Age must be more than 0");
            RuleFor(e => e.DepartmentId)
                .NotNull().WithMessage("You must provide Department of the Employee")
                .Must(deptId=>deptId > 0 ).WithMessage("Please Provide a valid Department Id"); 
        }
    }
}

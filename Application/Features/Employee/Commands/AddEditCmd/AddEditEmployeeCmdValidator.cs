using FluentValidation;

namespace CleanArchTask.Application.Features.Employee.Commands.AddEditCmd
{
    public class AddEditEmployeeCmdValidator:AbstractValidator<AddEditEmployeeCmd>
    {
        public AddEditEmployeeCmdValidator() 
        {
            RuleFor(e=>e.Id)
                .Must(id => id == null || id >= 0).WithMessage("Id Can't be Less Than 0");
            RuleFor(e => e.FullNameAr)
                .NotEmpty().WithMessage("You must provide The Arabic Name");
            RuleFor(e => e.FullNameEn)
                .NotEmpty().WithMessage("You must provide The English Name");
            RuleFor(e => e.Age)
                .NotNull().WithMessage("You must provide Age of the Employee")
                .Must(age=>age>0).WithMessage("Age must be more than 0");
            RuleFor(e => e.DepartmentId)
                .NotNull().WithMessage("You must provide Department of the Employee")
                .Must(deptId=>deptId > 0 ).WithMessage("Please Provide a valid Department Id"); 
        }
    }
}

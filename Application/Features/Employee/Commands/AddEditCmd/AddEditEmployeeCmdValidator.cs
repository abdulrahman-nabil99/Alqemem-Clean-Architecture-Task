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
            RuleFor(e => e.DepartmentAr)
                .NotEmpty().WithMessage("You must provide The Arabic Department");
            RuleFor(e => e.DepartmentEn)
                .NotEmpty().WithMessage("You must provide The English Department");
        }
    }
}

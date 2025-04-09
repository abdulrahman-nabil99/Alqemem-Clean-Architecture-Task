using FluentValidation;

namespace CleanArchTask.Application.Features.Employee.Commands.DeleteCmd
{
    public class DeleteEmployeesCmdValidator: AbstractValidator<DeleteEmployeesCmd>
    {
        public DeleteEmployeesCmdValidator()
        {
            RuleFor(cmd => cmd.Ids)
                .NotEmpty().WithMessage("You must provide at least one ID.")
                .Must(ids => ids.All(id => id > 0)).WithMessage("All IDs must be greater than 0.");
        }
    }
}

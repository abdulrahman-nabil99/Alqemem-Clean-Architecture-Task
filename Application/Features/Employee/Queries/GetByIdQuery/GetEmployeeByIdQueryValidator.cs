using FluentValidation;

namespace CleanArchTask.Application.Features.Employee.Queries.GetByIdQuery
{
    public class GetEmployeeByIdQueryValidator :AbstractValidator<GetEmployeeByIdQuery>
    {
        public GetEmployeeByIdQueryValidator()
        {
            RuleFor(cmd => cmd.Id)
                .NotEmpty().WithMessage("You must provide the ID.")
                .Must(id => id > 0).WithMessage("Id must be greater than 0.");
        }
    }
}

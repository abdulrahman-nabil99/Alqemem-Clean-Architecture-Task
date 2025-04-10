using CleanArchTask.Application.Interfaces.Respositories;
using CleanArchTask.Domain.Common.Enums;
using CleanArchTask.Domain.Common.Models;
using MediatR;

namespace CleanArchTask.Application.Features.Employee.Commands.DeleteCmd
{
    public class DeleteEmployeesCmd : DeleteModel, IRequest<Response<int>>
    {
    }

    public class DeleteEmployeesCmdHandler : IRequestHandler<DeleteEmployeesCmd, Response<int>>
    {
        private readonly IEmployeeRepository _repo;

        public DeleteEmployeesCmdHandler(IEmployeeRepository repo)
        {
            _repo = repo;
        }

        public async Task<Response<int>> Handle(DeleteEmployeesCmd request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _repo.DeleteEmployeesAsync(request);
                return new Response<int>((int)ResponseStatusCode.NoContent, true, result, null, "Saved successfully");
            }
            catch (Exception ex)
            {
                return new Response<int>((int)ResponseStatusCode.InternalServerError, ex.Message);
            }

        }
    }
}

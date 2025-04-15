using CleanArchTask.Application.Hubs;
using CleanArchTask.Application.Interfaces.Respositories;
using CleanArchTask.Domain.Common.Enums;
using CleanArchTask.Domain.Common.Models;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace CleanArchTask.Application.Features.Employee.Commands.DeleteCmd
{
    public class DeleteEmployeesCmd : DeleteModel, IRequest<Response<int>>
    {
    }

    public class DeleteEmployeesCmdHandler : IRequestHandler<DeleteEmployeesCmd, Response<int>>
    {
        private readonly IEmployeeRepository _repo;
        private readonly IHubContext<EmployeesHub> _hub;

        public DeleteEmployeesCmdHandler(IEmployeeRepository repo, IHubContext<EmployeesHub> hub)
        {
            _repo = repo;
            _hub = hub;
        }

        public async Task<Response<int>> Handle(DeleteEmployeesCmd request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _repo.DeleteEmployeesAsync(request);
                if (result>0)
                    await _hub.Clients.All.SendAsync("reload", true);

                return new Response<int>((int)ResponseStatusCode.NoContent, true, result, null, "Saved successfully");
            }
            catch (Exception ex)
            {
                return new Response<int>((int)ResponseStatusCode.InternalServerError, ex.Message);
            }

        }
    }
}

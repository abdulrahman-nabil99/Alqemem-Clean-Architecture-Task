using CleanArchTask.Application.Hubs;
using CleanArchTask.Application.Interfaces.Respositories;
using CleanArchTask.Domain.Common.Enums;
using CleanArchTask.Domain.Common.Models;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace CleanArchTask.Application.Features.Employee.Commands.AddEditCmd
{
    public class AddEditEmployeeCmd : IRequest<Response<int>>
    {
        public int? Id { get; set; }
        public string FNameAr {  get; set; }
        public string FNameEn {  get; set; }
        public string LNameAr {  get; set; }
        public string LNameEn {  get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public int Age {  get; set; }
        public int MaritalStatusId { get; set; }
        public int DepartmentId {  get; set; }
    }
    public class AddEditEmployeeCmdHandler : IRequestHandler<AddEditEmployeeCmd, Response<int>>
    {
        private readonly IEmployeeRepository _repo;
        private readonly IHubContext<EmployeesHub> _hub;

        public AddEditEmployeeCmdHandler(IEmployeeRepository repo,IHubContext<EmployeesHub> hub)
        {
            _repo = repo;
            _hub = hub;
        }

        public async Task<Response<int>> Handle(AddEditEmployeeCmd request, CancellationToken cancellationToken)
        {
            try
            {
                var checkResponse = await _repo.CheckEmailAndMobileExists(request.Email,request.Mobile,request.Id);
                if (checkResponse is not null) 
                    return new Response<int>((int) ResponseStatusCode.BadRequest,"Email is already in use");

                int result = 0;
                if (request.Id is null || request.Id <= 0)
                    result = await _repo.AddEmployeeAsync(request);
                else if (request.Id > 0)
                    result = await _repo.UpdateEmployeeAsync(request);

                if (result > 0)
                    await _hub.Clients.All.SendAsync("reload", true);

                return result switch
                {
                    > 0 =>
                    new Response<int>((int)(request.Id > 0 ? ResponseStatusCode.OK : ResponseStatusCode.Created), true, result, null, "Saved Successfully"),
                    -1 => new Response<int>((int)ResponseStatusCode.NotFound, "No Found"),
                    -2 => new Response<int>((int)ResponseStatusCode.BadRequest, "Bad Request"),
                    _ => new Response<int>((int)ResponseStatusCode.InternalServerError, "Please Try Again Later")
                };

            }
            catch (Exception ex)
            {
                return new Response<int>((int)ResponseStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}

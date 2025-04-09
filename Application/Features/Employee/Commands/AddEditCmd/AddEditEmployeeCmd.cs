using CleanArchTask.Application.Interfaces.Respositories;
using CleanArchTask.Domain.Common.Enums;
using CleanArchTask.Domain.Common.Models;
using MediatR;

namespace CleanArchTask.Application.Features.Employee.Commands.AddEditCmd
{
    public class AddEditEmployeeCmd : IRequest<Response<int>>
    {
        public int? Id { get; set; }
        public string FullNameAr {  get; set; }
        public string FullNameEn {  get; set; }
        public string DepartmentAr {  get; set; }
        public string DepartmentEn {  get; set; }
        // Required Data
    }
    public class AddEditEmployeeCmdHandler : IRequestHandler<AddEditEmployeeCmd, Response<int>>
    {
        private readonly IEmployeeRepository _repo;

        public AddEditEmployeeCmdHandler(IEmployeeRepository repo)
        {
            _repo = repo;
        }

        public async Task<Response<int>> Handle(AddEditEmployeeCmd request, CancellationToken cancellationToken)
        {
            try
            {
                int result = 0;
                if (request.Id is null || request.Id <= 0)
                    result = await _repo.AddEmployeeAsync(request);
                else if (request.Id > 0)
                    result = await _repo.UpdateEmployeeAsync(request);

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

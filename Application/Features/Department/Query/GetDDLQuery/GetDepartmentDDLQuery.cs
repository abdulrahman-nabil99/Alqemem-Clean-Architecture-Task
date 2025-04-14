using CleanArchTask.Application.Interfaces.Respositories;
using CleanArchTask.Domain.Common.Enums;
using CleanArchTask.Domain.Common.Models;
using MediatR;


namespace CleanArchTask.Application.Features.Department.Query.GetDDLQuery
{
    public class GetDepartmentDDLQuery : IRequest<Response<IEnumerable<DDLItem<int>>>>
    {
    }
    public class GetDepartmentDDLQueryHandler : IRequestHandler<GetDepartmentDDLQuery, Response<IEnumerable<DDLItem<int>>>>
    {
        private readonly IDepartmentRepository _repo;

        public GetDepartmentDDLQueryHandler(IDepartmentRepository repo)
        {
            _repo = repo;
        }

        public async Task<Response<IEnumerable<DDLItem<int>>>> Handle(GetDepartmentDDLQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var departments = await _repo.GetDepartmentDDL();
                return new Response<IEnumerable<DDLItem<int>>>((int)ResponseStatusCode.OK, true, departments, 1, "Data Retrieved");
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<DDLItem<int>>>((int)ResponseStatusCode.InternalServerError, ex.Message);
            }

        }
    }
}

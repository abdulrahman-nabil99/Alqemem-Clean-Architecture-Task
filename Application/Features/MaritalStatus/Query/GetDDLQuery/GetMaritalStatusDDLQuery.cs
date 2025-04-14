using CleanArchTask.Application.Interfaces.Respositories;
using CleanArchTask.Domain.Common.Enums;
using CleanArchTask.Domain.Common.Models;
using MediatR;

namespace CleanArchTask.Application.Features.MaritalStatus.Query.GetDDLQuery
{
    public class GetMaritalStatusDDLQuery : IRequest<Response<IEnumerable<DDLItem<int>>>>
    {
    }

    public class GetMaritalStatusDDLQueryHandler : IRequestHandler<GetMaritalStatusDDLQuery, Response<IEnumerable<DDLItem<int>>>>
    {
        private readonly IMaritalStatusRepository _repo;

        public GetMaritalStatusDDLQueryHandler(IMaritalStatusRepository repo)
        {
            _repo = repo;
        }

        public async Task<Response<IEnumerable<DDLItem<int>>>> Handle(GetMaritalStatusDDLQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var maritalStatuses = await _repo.GetMaritalStatusDDL();
                return new Response<IEnumerable<DDLItem<int>>>((int)ResponseStatusCode.OK, true, maritalStatuses, 1, "Data Retrieved");
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<DDLItem<int>>>((int)ResponseStatusCode.InternalServerError, ex.Message);
            }

        }
    }
}

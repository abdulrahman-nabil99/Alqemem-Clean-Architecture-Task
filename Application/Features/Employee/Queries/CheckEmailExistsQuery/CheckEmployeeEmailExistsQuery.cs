using CleanArchTask.Application.Interfaces.Respositories;
using MediatR;

namespace CleanArchTask.Application.Features.Employee.Queries.CheckEmailExistsQuery
{
    public class CheckEmployeeEmailExistsQuery: IRequest<bool>
    {
        public string Value { get; set; }
        public int? Id { get; set; }
    }
    public class CheckEmployeeEmailExistsQueryHandler : IRequestHandler<CheckEmployeeEmailExistsQuery, bool>
    {
        private readonly IEmployeeRepository _repo;

        public CheckEmployeeEmailExistsQueryHandler(IEmployeeRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(CheckEmployeeEmailExistsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _repo.CheckEmailExists(request.Value,request.Id);
                return result;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}

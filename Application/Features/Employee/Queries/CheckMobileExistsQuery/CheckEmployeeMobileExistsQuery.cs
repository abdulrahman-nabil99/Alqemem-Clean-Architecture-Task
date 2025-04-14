using CleanArchTask.Application.Interfaces.Respositories;
using MediatR;

namespace CleanArchTask.Application.Features.Employee.Queries.CheckEmailExistsQuery
{
    public class CheckEmployeeMobileExistsQuery: IRequest<bool>
    {
        public string Value { get; set; }
        public int? Id { get; set; }
    }
    public class CheckEmployeeMobileExistsQueryHandler : IRequestHandler<CheckEmployeeMobileExistsQuery, bool>
    {
        private readonly IEmployeeRepository _repo;

        public CheckEmployeeMobileExistsQueryHandler(IEmployeeRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(CheckEmployeeMobileExistsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _repo.CheckMobileExists(request.Value,request.Id);
                return result;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}

using iTicket.Application.Bases;
using iTicket.Application.Interfaces.AutoMapper;
using iTicket.Application.Interfaces.UnitOfWorks;
using iTicket.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace iTicket.Application.Features.Employees.Queries.GetAllEmployeesByPaging
{
    public class GetAllEmployeesByPagingQueryHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IHttpContextAccessor httpContextAccessor) : BaseHandler(mapper, unitOfWork, httpContextAccessor), IRequestHandler<GetAllEmployeesByPagingQueryRequest, IList<GetAllEmployeesByPagingQueryResponse>>

    {
        public async Task<IList<GetAllEmployeesByPagingQueryResponse>> Handle(GetAllEmployeesByPagingQueryRequest request, CancellationToken cancellationToken)
        {
            IList<Employee> employess = await unitOfWork
                .GetReadRepository<Employee>()
                .GetAllByPagingAsync(
                currentPage: request.CurrentPage,
                pageSize: request.PageSize,
                predicate: x => x.CompanyId == request.CompanyId
                && x.IsDeleted == request.IsDeleted);

            return mapper.Map<GetAllEmployeesByPagingQueryResponse, Employee>(employess);
        }
    }
}

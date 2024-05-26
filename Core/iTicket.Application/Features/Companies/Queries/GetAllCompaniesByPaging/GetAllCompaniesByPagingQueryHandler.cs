using iTicket.Application.Bases;
using iTicket.Application.Interfaces.AutoMapper;
using iTicket.Application.Interfaces.UnitOfWorks;
using iTicket.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace iTicket.Application.Features.Companies.Queries.GetAllCompaniesByPaging
{
    public class GetAllCompaniesByPagingQueryHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IHttpContextAccessor httpContextAccessor) : BaseHandler(mapper, unitOfWork, httpContextAccessor), IRequestHandler<GetAllCompaniesByPagingQueryRequest, IList<GetAllCompaniesByPagingQueryResponse>>
    {
        public async Task<IList<GetAllCompaniesByPagingQueryResponse>> Handle(GetAllCompaniesByPagingQueryRequest request, CancellationToken cancellationToken)
        {   
            IList<Company> companies = await unitOfWork.GetReadRepository<Company>()
                 .GetAllByPagingAsync(
                currentPage: request.CurrentPage, 
                pageSize: request.PageSize,
                predicate: x => !x.IsDeleted);

            return mapper.Map<GetAllCompaniesByPagingQueryResponse, Company>(companies);
        }
    }

}

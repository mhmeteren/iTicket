using iTicket.Application.Bases;
using iTicket.Application.Interfaces.RedisCache;
using MediatR;

namespace iTicket.Application.Features.Companies.Queries.GetAllCompaniesByPaging
{
    public class GetAllCompaniesByPagingQueryRequest : BaseRequestForPaging, IRequest<IList<GetAllCompaniesByPagingQueryResponse>>, ICacheableQuery
    {
        public bool IsDeleted { get; set; } = false;

        public string CacheKey => $"GetAllCompaniesByPaging::CurrentPage:{CurrentPage}::PageSize:{PageSize}";

        public double CacheTime => 5;
    }

}

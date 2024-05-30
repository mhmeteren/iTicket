using iTicket.Application.Bases;
using MediatR;

namespace iTicket.Application.Features.Companies.Queries.GetAllCompaniesByPaging
{
    public class GetAllCompaniesByPagingQueryRequest: BaseRequestForPaging, IRequest<IList<GetAllCompaniesByPagingQueryResponse>>
    {
        public bool IsDeleted { get; set; } = false;
    }

}

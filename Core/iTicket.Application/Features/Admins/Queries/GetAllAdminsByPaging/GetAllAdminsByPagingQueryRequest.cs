using iTicket.Application.Bases;
using MediatR;

namespace iTicket.Application.Features.Admins.Queries.GetAllAdminsByPaging
{
    public class GetAllAdminsByPagingQueryRequest : BaseRequestForPaging, IRequest<IList<GetAllAdminsByPagingQueryResponse>>
    {
        public bool IsDeleted { get; set; } = false;
    }
}

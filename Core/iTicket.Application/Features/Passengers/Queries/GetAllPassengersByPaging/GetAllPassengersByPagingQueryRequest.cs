using iTicket.Application.Bases;
using MediatR;

namespace iTicket.Application.Features.Passengers.Queries.GetAllPassengersByPaging
{
    public class GetAllPassengersByPagingQueryRequest : BaseRequestForPaging, IRequest<IList<GetAllPassengersByPagingQueryResponse>>
    {
    }
}

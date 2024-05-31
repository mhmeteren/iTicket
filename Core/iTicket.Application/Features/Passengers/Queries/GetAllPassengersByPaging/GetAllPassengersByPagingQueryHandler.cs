using iTicket.Application.Bases;
using iTicket.Application.Features.Passengers.Rules;
using iTicket.Application.Interfaces.AutoMapper;
using iTicket.Application.Interfaces.UnitOfWorks;
using iTicket.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace iTicket.Application.Features.Passengers.Queries.GetAllPassengersByPaging
{
    public class GetAllPassengersByPagingQueryHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IHttpContextAccessor httpContextAccessor,
        UserManager<BaseUser> userManager,
        PassengerRules passengerRules) : BaseHandler(mapper, unitOfWork, httpContextAccessor), IRequestHandler<GetAllPassengersByPagingQueryRequest, IList<GetAllPassengersByPagingQueryResponse>>
    {
        private readonly UserManager<BaseUser> userManager = userManager;
        private readonly PassengerRules passengerRules = passengerRules;

        public async Task<IList<GetAllPassengersByPagingQueryResponse>> Handle(GetAllPassengersByPagingQueryRequest request, CancellationToken cancellationToken)
        {
            User? user = await userManager.FindByIdAsync(UserId) as User;
            await passengerRules.OwnerUserIsDeleted(user);

            IList<Passenger> passengers = await unitOfWork
                .GetReadRepository<Passenger>()
                .GetAllByPagingAsync(
                currentPage: request.CurrentPage,
                pageSize: request.PageSize,
                predicate: x => x.UserId.Equals(user.Id) && !x.IsDeleted,
                orderBy: x => x.OrderBy(i => i.Priority));

            return mapper.Map<GetAllPassengersByPagingQueryResponse, Passenger>(passengers);
        }
    }
}

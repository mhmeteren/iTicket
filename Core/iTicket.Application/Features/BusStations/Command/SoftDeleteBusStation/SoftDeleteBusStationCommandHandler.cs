using iTicket.Application.Bases;
using iTicket.Application.Features.BusStations.Rules;
using iTicket.Application.Interfaces.AutoMapper;
using iTicket.Application.Interfaces.UnitOfWorks;
using iTicket.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace iTicket.Application.Features.BusStations.Command.SoftDeleteBusStation
{
    public class SoftDeleteBusStationCommandHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IHttpContextAccessor httpContextAccessor,
        BusStationRules busStationRules) : BaseHandler(mapper, unitOfWork, httpContextAccessor), IRequestHandler<SoftDeleteBusStationCommandRequest, Unit>
    {
        private readonly BusStationRules busStationRules = busStationRules;

        public async Task<Unit> Handle(SoftDeleteBusStationCommandRequest request, CancellationToken cancellationToken)
        {
            BusStation? busStation = await unitOfWork
                .GetReadRepository<BusStation>().GetAsync(predicate: x => x.Id == request.Id);

            await busStationRules.BusStationShouldBeExists(busStation);

            busStation.IsDeleted = true;

            await unitOfWork.GetWriteRepository<BusStation>().UpdateAsync(busStation);
            await unitOfWork.SaveAsync();
            return Unit.Value;
        }
    }


}

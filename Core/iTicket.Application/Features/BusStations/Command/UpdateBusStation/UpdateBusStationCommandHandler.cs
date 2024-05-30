using iTicket.Application.Bases;
using iTicket.Application.Features.BusStations.Rules;
using iTicket.Application.Interfaces.AutoMapper;
using iTicket.Application.Interfaces.UnitOfWorks;
using iTicket.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace iTicket.Application.Features.BusStations.Command.UpdateBusStation
{
    public class UpdateBusStationCommandHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IHttpContextAccessor httpContextAccessor,
        BusStationRules busStationRules) : BaseHandler(mapper, unitOfWork, httpContextAccessor), IRequestHandler<UpdateBusStationCommandRequest, Unit>
    {
        private readonly BusStationRules busStationRules = busStationRules;

        public async Task<Unit> Handle(UpdateBusStationCommandRequest request, CancellationToken cancellationToken)
        {
            BusStation? busStation = await unitOfWork
                .GetReadRepository<BusStation>().GetAsync(predicate: x=> x.Id == request.Id);

            await busStationRules.BusStationShouldBeExists(busStation);

            BusStation? busStationCheck = await unitOfWork
                .GetReadRepository<BusStation>().GetAsync(predicate: x => x.Name.Equals(request.Name) && x.City.Equals(request.City));

            await busStationRules.BusStationShouldBeUniqueIgnoreOwn(busStationCheck, busStation.Id);

            mapper.Map(request, busStation);
            await unitOfWork.GetWriteRepository<BusStation>().UpdateAsync(busStation);
            await unitOfWork.SaveAsync();
            return Unit.Value;
        }
    }


}

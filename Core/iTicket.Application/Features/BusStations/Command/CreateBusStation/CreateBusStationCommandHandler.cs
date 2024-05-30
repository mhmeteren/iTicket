using iTicket.Application.Bases;
using iTicket.Application.Features.BusStations.Rules;
using iTicket.Application.Interfaces.AutoMapper;
using iTicket.Application.Interfaces.UnitOfWorks;
using iTicket.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace iTicket.Application.Features.BusStations.Command.CreateBusStation
{
    public class CreateBusStationCommandHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IHttpContextAccessor httpContextAccessor,
        BusStationRules busStationRules) : BaseHandler(mapper, unitOfWork, httpContextAccessor), IRequestHandler<CreateBusStationCommandRequest, Unit>
    {
        private readonly BusStationRules busStationRules = busStationRules;

        public async Task<Unit> Handle(CreateBusStationCommandRequest request, CancellationToken cancellationToken)
        {
            BusStation? busStationCheck = await unitOfWork
                .GetReadRepository<BusStation>().GetAsync(predicate: x => x.Name.Equals(request.Name) && x.City.Equals(request.City));

            await busStationRules.BusStationShouldBeUnique(busStationCheck);

            BusStation busStation = mapper.Map<BusStation, CreateBusStationCommandRequest>(request);
            await unitOfWork.GetWriteRepository<BusStation>().AddAsync(busStation);
            await unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}

using iTicket.Application.Bases;
using iTicket.Application.Features.Passengers.Rules;
using iTicket.Application.Interfaces.AutoMapper;
using iTicket.Application.Interfaces.UnitOfWorks;
using iTicket.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace iTicket.Application.Features.Passengers.Command.CreatePassenger
{
    public class CreatePassengerCommandHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IHttpContextAccessor httpContextAccessor,
        UserManager<BaseUser> userManager,
        PassengerRules passengerRules) : BaseHandler(mapper, unitOfWork, httpContextAccessor), IRequestHandler<CreatePassengerCommandRequest, Unit>

    {
        private readonly UserManager<BaseUser> userManager = userManager;
        private readonly PassengerRules passengerRules = passengerRules;

        public async Task<Unit> Handle(CreatePassengerCommandRequest request, CancellationToken cancellationToken)
        {
            User? user = await userManager.FindByIdAsync(UserId) as User;
            await passengerRules.OwnerUserIsDeleted(user);

            IList<Passenger> passengers = await unitOfWork
                .GetReadRepository<Passenger>()
                .GetAllAsync(predicate: x => x.UserId.Equals(user.Id) && !x.IsDeleted);

            await passengerRules.IsPassengerLimitExceeded(passengers.Count);
            await passengerRules.IdentificationNoOrPassportNoShouldBeUnique(passengers, request);

            Passenger passenger = mapper.Map<Passenger, CreatePassengerCommandRequest>(request);
            passenger.Priority = passengers.Count;
            passenger.UserId = user.Id;

            await unitOfWork.GetWriteRepository<Passenger>().AddAsync(passenger);
            await unitOfWork.SaveAsync();
            return Unit.Value;
        }
    }
}

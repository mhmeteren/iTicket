using iTicket.Application.Bases;
using iTicket.Application.Features.Passengers.Rules;
using iTicket.Application.Interfaces.AutoMapper;
using iTicket.Application.Interfaces.UnitOfWorks;
using iTicket.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace iTicket.Application.Features.Passengers.Command.SoftDeletePassenger
{
    public class SoftDeletePassengerCommandHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IHttpContextAccessor httpContextAccessor,
        UserManager<BaseUser> userManager,
        PassengerRules passengerRules) : BaseHandler(mapper, unitOfWork, httpContextAccessor), IRequestHandler<SoftDeletePassengerCommandRequest, Unit>
    {
        private readonly UserManager<BaseUser> userManager = userManager;
        private readonly PassengerRules passengerRules = passengerRules;

        public async Task<Unit> Handle(SoftDeletePassengerCommandRequest request, CancellationToken cancellationToken)
        {
            User? user = await userManager.FindByIdAsync(UserId) as User;
            await passengerRules.OwnerUserIsDeleted(user);

            Passenger? passenger = await unitOfWork
               .GetReadRepository<Passenger>()
               .GetAsync(predicate: x => x.Id == request.Id && x.UserId.Equals(user.Id) && !x.IsDeleted);
            await passengerRules.PassengerIsDeleted(passenger);
            await passengerRules.PassengerShuoldNotBePrimary(passenger);

            passenger.IsDeleted = true;

            await unitOfWork.GetWriteRepository<Passenger>().UpdateAsync(passenger);
            await unitOfWork.SaveAsync();
            return Unit.Value;
        }
    }


}

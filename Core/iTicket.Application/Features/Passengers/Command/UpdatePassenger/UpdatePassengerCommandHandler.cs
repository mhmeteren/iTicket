using iTicket.Application.Bases;
using iTicket.Application.Features.Passengers.Rules;
using iTicket.Application.Interfaces.AutoMapper;
using iTicket.Application.Interfaces.UnitOfWorks;
using iTicket.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace iTicket.Application.Features.Passengers.Command.UpdatePassenger
{
    public class UpdatePassengerCommandHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IHttpContextAccessor httpContextAccessor,
        UserManager<BaseUser> userManager,
        PassengerRules passengerRules) : BaseHandler(mapper, unitOfWork, httpContextAccessor), IRequestHandler<UpdatePassengerCommandRequest, Unit>
    {
        private readonly UserManager<BaseUser> userManager = userManager;
        private readonly PassengerRules passengerRules = passengerRules;

        public async Task<Unit> Handle(UpdatePassengerCommandRequest request, CancellationToken cancellationToken)
        {
            User? user = await userManager.FindByIdAsync(UserId) as User;
            await passengerRules.OwnerUserIsDeleted(user);


            IList<Passenger> passengers = await unitOfWork
               .GetReadRepository<Passenger>()
               .GetAllAsync(predicate: x => x.UserId.Equals(user.Id) && !x.IsDeleted);

            Passenger? passenger = passengers.Where(x => x.Id == request.Id).FirstOrDefault();
            await passengerRules.PassengerIsDeleted(passenger);

            await passengerRules.IdentificationNoOrPassportNoShouldBeUniqueIgnoreOwn(passengers, request);



            mapper.Map(request, passenger);
            await unitOfWork.GetWriteRepository<Passenger>().UpdateAsync(passenger);
            await unitOfWork.SaveAsync();
            return Unit.Value;
        }
    }


}

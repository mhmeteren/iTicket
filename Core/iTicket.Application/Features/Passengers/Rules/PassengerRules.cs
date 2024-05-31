using iTicket.Application.Bases;
using iTicket.Application.Features.Passengers.Command.CreatePassenger;
using iTicket.Application.Features.Passengers.Command.UpdatePassenger;
using iTicket.Application.Features.Passengers.Exceptions;
using iTicket.Domain.Entities;

namespace iTicket.Application.Features.Passengers.Rules
{
    public class PassengerRules : BaseRules
    {
        
        public Task PassengerIsDeleted(Passenger? passenger)
        {
            if (passenger is null || passenger.IsDeleted) throw new PassengerNotFoundException();
            return Task.CompletedTask;
        }


        public Task PassengerShuoldNotBePrimary(Passenger passenger)
        {
            if (passenger.Priority == 0) throw new PassengerShuoldNotBePrimaryException();
            return Task.CompletedTask;
        }

        public Task OwnerUserIsDeleted(User? owner)
        {
            if (owner is null || owner.IsDeleted) throw new OwnerUserIsDeletedException();
            return Task.CompletedTask;
        }

        public Task IsPassengerLimitExceeded(int count)
        {
            if (count >= 10) throw new IsPassengerLimitExceededException();
            return Task.CompletedTask;
        }


        public Task IdentificationNoOrPassportNoShouldBeUnique(IList<Passenger> passengers, CreatePassengerCommandRequest request)
        {
            return request.IsNotTurkishCitizen ?
                 PassportNoShouldBeUnique(passengers, request.PassportNo) :
                 IdentificationShouldBeUnique(passengers, request.IdentificationNo);
        }

        public Task IdentificationShouldBeUnique(IList<Passenger> passengers, string IdentificationNo)
        {
            if (passengers.Count(x => !x.IsNotTurkishCitizen && x.IdentificationNo.Equals(IdentificationNo)) > 0)
                throw new IdentificationNoShouldBeUniqueException();

            return Task.CompletedTask;
        }

        public Task PassportNoShouldBeUnique(IList<Passenger> passengers, string PassportNo)
        {
            if (passengers.Count(x => x.IsNotTurkishCitizen && x.PassportNo.Equals(PassportNo)) > 0)
                throw new PassportNoShouldBeUniqueException();

            return Task.CompletedTask;
        }


        public Task IdentificationNoOrPassportNoShouldBeUniqueIgnoreOwn(IList<Passenger> passengers, UpdatePassengerCommandRequest request)
        {
            return request.IsNotTurkishCitizen ?
                 PassportNoShouldBeUniqueIgnoreOwn(passengers, request) :
                 IdentificationShouldBeUniqueIgnoreOwn(passengers, request);
        }

        public Task IdentificationShouldBeUniqueIgnoreOwn(IList<Passenger> passengers, UpdatePassengerCommandRequest request)
        {
            if (passengers.Count(x => x.Id != request.Id && !x.IsNotTurkishCitizen && x.IdentificationNo.Equals(request.IdentificationNo)) > 0)
                throw new IdentificationNoShouldBeUniqueException();

            return Task.CompletedTask;
        }

        public Task PassportNoShouldBeUniqueIgnoreOwn(IList<Passenger> passengers, UpdatePassengerCommandRequest request)
        {
            if (passengers.Count(x => x.Id != request.Id && x.IsNotTurkishCitizen && x.PassportNo.Equals(request.PassportNo)) > 0)
                throw new PassportNoShouldBeUniqueException();

            return Task.CompletedTask;
        }

    }
}

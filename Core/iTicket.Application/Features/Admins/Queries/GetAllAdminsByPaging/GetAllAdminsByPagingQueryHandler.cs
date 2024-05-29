using iTicket.Application.Bases;
using iTicket.Application.Interfaces.AutoMapper;
using iTicket.Application.Interfaces.UnitOfWorks;
using iTicket.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace iTicket.Application.Features.Admins.Queries.GetAllAdminsByPaging
{
    public class GetAllAdminsByPagingQueryHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IHttpContextAccessor httpContextAccessor) : BaseHandler(mapper, unitOfWork, httpContextAccessor),
        IRequestHandler<GetAllAdminsByPagingQueryRequest, IList<GetAllAdminsByPagingQueryResponse>>
    {
        public async Task<IList<GetAllAdminsByPagingQueryResponse>> Handle(GetAllAdminsByPagingQueryRequest request, CancellationToken cancellationToken)
        {
            IList<Admin> admins = await unitOfWork.GetReadRepository<Admin>()
                  .GetAllByPagingAsync(
                 currentPage: request.CurrentPage,
                 pageSize: request.PageSize,
                 predicate: x => x.IsDeleted == request.IsDeleted);

            return mapper.Map<GetAllAdminsByPagingQueryResponse, Admin>(admins);
        }
    }
}

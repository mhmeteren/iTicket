using iTicket.Application.Interfaces.AutoMapper;
using iTicket.Application.Interfaces.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace iTicket.Application.Bases
{
    public class BaseHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
    {
        public readonly IMapper mapper = mapper;
        public readonly IUnitOfWork unitOfWork = unitOfWork;
        public readonly IHttpContextAccessor httpContextAccessor = httpContextAccessor;
        public readonly string? UserId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}

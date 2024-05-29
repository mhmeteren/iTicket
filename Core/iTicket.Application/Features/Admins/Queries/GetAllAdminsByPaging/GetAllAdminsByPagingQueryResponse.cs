namespace iTicket.Application.Features.Admins.Queries.GetAllAdminsByPaging
{
    public record GetAllAdminsByPagingQueryResponse
    {
        public Guid Id { get; init; }
        public string Email { get; init; }


        public DateTime DeleteDate { get; init; }
        public bool IsDeleted { get; init; }
        public DateTime CreateDate { get; init; }
    }
}

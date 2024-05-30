namespace iTicket.Application.Features.Companies.Queries.GetAllCompaniesByPaging
{
    public record GetAllCompaniesByPagingQueryResponse
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string LogoUrl { get; init; }
    }

}

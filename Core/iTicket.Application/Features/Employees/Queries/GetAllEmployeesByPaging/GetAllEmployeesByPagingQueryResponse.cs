namespace iTicket.Application.Features.Employees.Queries.GetAllEmployeesByPaging
{
    public record GetAllEmployeesByPagingQueryResponse
	{
        public string FullName { get; init; }
        public string Email { get; init; }
    }
}

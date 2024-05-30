using iTicket.Application.Bases;
using MediatR;

namespace iTicket.Application.Features.Employees.Queries.GetAllEmployeesByPaging
{
    public class GetAllEmployeesByPagingQueryRequest : BaseRequestForPaging, IRequest<IList<GetAllEmployeesByPagingQueryResponse>>
    {
		private int companyId;

		public int CompanyId
        {
			get { return companyId; }
			set { companyId = value <= 0 ? 0 : value; }
		}

        public bool IsDeleted { get; set; } = false;
    }
}

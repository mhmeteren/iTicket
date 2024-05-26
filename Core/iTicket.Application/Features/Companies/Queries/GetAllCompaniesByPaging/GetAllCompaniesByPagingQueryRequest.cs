using MediatR;

namespace iTicket.Application.Features.Companies.Queries.GetAllCompaniesByPaging
{
    public class GetAllCompaniesByPagingQueryRequest: IRequest<IList<GetAllCompaniesByPagingQueryResponse>>
    {
        private int _currentPage = 1;

        public int CurrentPage
        {
            get { return _currentPage; }
            set { _currentPage = value > 0 ? value : _currentPage; }
        }

        private int _pagesize = 5;

        public int PageSize
        {
            get { return _pagesize; }
            set { _pagesize = value is > 0 and <= 20 ? value : _pagesize; }
        }
    }

}

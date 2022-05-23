using OnlineLearning.Settings;

using RestSharp;

namespace OnlineLearning.Common
{
    public abstract class QueryStringParameters
    {
        const int maxPageSize = 50;
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 10;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
        public virtual void HandleSettings(PaginationSettings paginationSettings)
        {
            int defaultPageNumber = paginationSettings.DefaultPageNumber;
            int maxPageSize = paginationSettings.MaxPageSize;
            int defaultPageSize = paginationSettings.DefaultPageSize;
            if (PageNumber < 1)
                PageNumber = defaultPageNumber;
            if (PageSize < 1)
                PageSize = defaultPageSize;
            if (PageSize > maxPageSize)
                PageSize = maxPageSize;
        }
    }
}

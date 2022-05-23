namespace OnlineLearning.Common
{
    public class PagedListResponseModel<T>
    {
        public PagedList<T> List { get; set; }
        public PagedListMetaData MetaData => List?.MetaData;
    }
    public class PagedListMetaData
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;
    }
}

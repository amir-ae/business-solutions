namespace BusinessSolutions.Services.Ordering.API.Contract.Responses
{
    public class PaginatedResponse<TEntity> where TEntity : class
    {
        public PaginatedResponse(int pageIndex, int pageSize, long total, IEnumerable<TEntity> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Total = total;
            Data = data;
        }

        public int PageIndex { get; }
        public int PageSize { get; }
        public long Total { get; }
        public IEnumerable<TEntity> Data { get; }
    }
}

namespace BusinessSolutions.Services.Ordering.API.Client.Base
{
    public interface IBaseClient
    {
        Task<T> GetAsync<T>(Uri uri, CancellationToken cancellationToken);
        Task<T> PostAsJson<T, U>(Uri uri, U data, CancellationToken cancellationToken);
        Task<T> PutAsJson<T, U>(Uri uri, U data, CancellationToken cancellationToken);
        Task DeleteAsync(Uri uri, CancellationToken cancellationToken);
        Task<T> ReadContentAs<T>(HttpResponseMessage response);
        Uri BuildUri(string format, Dictionary<string, string>? dict = default);
    }
}
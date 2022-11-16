namespace BusinessSolutions.Services.Ordering.Domain.Repositories
{
    public interface IRepository
    {
        IUnItOfWork UnitOfWork { get; }
    }
}

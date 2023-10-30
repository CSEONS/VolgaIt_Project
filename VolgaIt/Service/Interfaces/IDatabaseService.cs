namespace VolgaIt.Service.Interfaces
{
    public interface IDatabaseService
    {
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }
}

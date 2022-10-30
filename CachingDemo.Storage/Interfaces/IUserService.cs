namespace CachingDemo.Storage.Interfaces;

public interface IUserService
{
    Task<User> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<User> CreateAsync(string name, string email, CancellationToken cancellationToken = default);
}
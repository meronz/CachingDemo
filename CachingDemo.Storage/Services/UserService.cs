namespace CachingDemo.Storage.Services;

public class UserService : IUserService
{
    private readonly AppDbContext _dbContext;

    public UserService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<User> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return _dbContext.Set<User>().FirstAsync(_ => _.Id == id, cancellationToken);
    }

    public async Task<User> CreateAsync(string name, string email, CancellationToken cancellationToken = default)
    {
        var user = new User { Id = Guid.NewGuid().ToString(), Name = name, Email = email };
        await _dbContext.Set<User>().AddAsync(user, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return user;
    }
}
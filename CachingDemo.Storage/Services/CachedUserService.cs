using ZiggyCreatures.Caching.Fusion;

namespace CachingDemo.Storage.Services;

public class CachedUserService : IUserService
{
    private readonly IFusionCache _cache;
    private readonly UserService _userService;

    public CachedUserService(
        IFusionCache cache,
        UserService userService)
    {
        _cache = cache;
        _userService = userService;
    }

    public async Task<User> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        var key = $"user-{id}";
        return await _cache.GetOrSetAsync(key,
            _ => _userService.GetByIdAsync(id, cancellationToken),
            token: cancellationToken);
    }

    public async Task<User> CreateAsync(string name, string email, CancellationToken cancellationToken = default)
    {
        var user = await _userService.CreateAsync(name, email, cancellationToken);
        var key = $"user-{user.Id}";
        await _cache.SetAsync(key, user, token: cancellationToken);
        return user;
    }
}
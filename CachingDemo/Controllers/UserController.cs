using Microsoft.AspNetCore.Mvc;

namespace CachingDemo.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _users;

    public UserController(IUserService users)
    {
        _users = users;
    }

    [HttpGet("{id}")]
    public async Task<User> Get(string id)
    {
        return await _users.GetByIdAsync(id, HttpContext.RequestAborted);
    }

    [HttpPost]
    public async Task<IActionResult> Create(string? name, string? email)
    {
        if (string.IsNullOrEmpty(name)) return BadRequest("Name must not be null!");
        if (string.IsNullOrEmpty(email)) return BadRequest("Email must not be null!");

        var user = await _users.CreateAsync(name, email, HttpContext.RequestAborted);
        return CreatedAtAction(nameof(Get), routeValues: new { id = user.Id }, user);
    }
}
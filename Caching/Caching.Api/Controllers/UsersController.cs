using Caching.Application.Common.Identity.Services;
using Caching.Domain.Common.Query;
using Caching.Domain.Entities;
using Caching.Domain.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Caching.Api.Controllers;


[ApiController]
[Route("api/[controller]")]
public class UsersController(IUserService userService) : ControllerBase
{
    [HttpGet]
    public async ValueTask<IActionResult> GetById([FromQuery] FilterPagination filterPagination)
    {
        var specification = new QuerySpecification<User>(filterPagination.PageSize, filterPagination.PageToken);
        var result = await userService.GetAsync(specification);

        return result.Any() ? Ok(result) : NotFound();
    }

    [HttpGet("{userId:guid}")]
    public async ValueTask<IActionResult> GetById([FromRoute] Guid userId)
    {
        var result = await userService.GetByIdAsync(userId, asNoTracking: true);
        return result is not null ? Ok(result) : NotFound();
    }
}
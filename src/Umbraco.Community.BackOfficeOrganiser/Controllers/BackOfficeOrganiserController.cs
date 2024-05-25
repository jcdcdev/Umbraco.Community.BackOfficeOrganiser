using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core;
using Umbraco.Cms.Web.Common.Attributes;
using Umbraco.Cms.Web.Common.Authorization;
using Umbraco.Cms.Web.Common.Filters;
using Umbraco.Community.BackOfficeOrganiser.Models;

namespace Umbraco.Community.BackOfficeOrganiser.Controllers;

[IsBackOffice]
[UmbracoUserTimeoutFilter]
// [Authorize(Policy = AuthorizationPolicies.BackOfficeAccess)]
[DisableBrowserCache]
[ApiController]
[Route("umbraco/backofficeorganiser")]
public class BackOfficeOrganiserController : ControllerBase
{
    private readonly IBackOfficeOrganiserService _service;

    public BackOfficeOrganiserController(IBackOfficeOrganiserService service)
    {
        _service = service;
    }

    [HttpGet]
    [Route("organise")]
    public async Task<IActionResult> Organise([FromQuery] OrganiseRequest model)
    {
        var success = true;
        foreach (var type in model.Types.Select(DetermineOrganiseType).Distinct())
        {
            var attempt = await _service.OrganiseAsync(type);
            if (!attempt.Success)
            {
                success = false;
            }
        }

        if (!success)
        {
            return BadRequest(OrganiseResponse.Fail("Failed to organise"));
        }

        return Ok(OrganiseResponse.Success($"Successfully organised 🚀"));
    }

    private static OrganiseType DetermineOrganiseType(string input)
    {
        int.TryParse(input, out var value);
        return (OrganiseType)value;
    }
}
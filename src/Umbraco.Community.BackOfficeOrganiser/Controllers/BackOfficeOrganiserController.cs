using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Web.Common.Authorization;
using Umbraco.Cms.Web.Common.Routing;
using Umbraco.Community.BackOfficeOrganiser.Models;
using Umbraco.Community.BackOfficeOrganiser.Services;

namespace Umbraco.Community.BackOfficeOrganiser.Controllers;

[ApiController]
[BackOfficeRoute("backofficeorganiser/api")]
[Authorize(Policy = AuthorizationPolicies.BackOfficeAccess)]
public class BackOfficeOrganiserController(IBackOfficeOrganiserService service) : ControllerBase
{
    [HttpPost("organise")]
    [Produces(typeof(OrganiseResponse))]
    [Consumes(typeof(OrganiseRequest), "application/json")]
    public async Task<IActionResult> Organise([FromBody] OrganiseRequest model)
    {
        var success = true;
        foreach (var type in model.GetOrganiseTypes())
        {
            var attempt = await service.OrganiseAsync(type);
            if (!attempt.Success)
            {
                success = false;
            }
        }

        if (!success)
        {
            return BadRequest(OrganiseResponse.Fail("Failed to organise"));
        }

        return Ok(OrganiseResponse.Success("Successfully organised \ud83d\ude80"));
    }

    private static OrganiseType DetermineOrganiseType(string input)
    {
        int.TryParse(input, out var value);
        return (OrganiseType)value;
    }
}
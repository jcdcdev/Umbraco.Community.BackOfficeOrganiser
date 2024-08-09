using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Web.BackOffice.Filters;
using Umbraco.Cms.Web.Common.Attributes;
using Umbraco.Cms.Web.Common.Authorization;
using Umbraco.Cms.Web.Common.Controllers;
using Umbraco.Cms.Web.Common.Filters;
using Umbraco.Community.BackOfficeOrganiser.Models;

namespace Umbraco.Community.BackOfficeOrganiser.Controllers;

[IsBackOffice]
[UmbracoUserTimeoutFilter]
[Authorize(Policy = AuthorizationPolicies.BackOfficeAccess)]
[DisableBrowserCache]
[UmbracoRequireHttps]
public class BackOfficeOrganiserController(IBackOfficeOrganiserService service) : UmbracoApiController
{
    [HttpPost]
    public IActionResult Organise(OrganiseRequest model)
    {
        var success = true;
        foreach (var type in model.Types.Select(DetermineOrganiseType).Distinct())
        {
            var attempt = service.Organise(type);
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
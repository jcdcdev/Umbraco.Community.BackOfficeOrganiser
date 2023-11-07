using jcdcdev.Umbraco.BackOfficeOrganiser.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Web.BackOffice.Filters;
using Umbraco.Cms.Web.Common.Attributes;
using Umbraco.Cms.Web.Common.Authorization;
using Umbraco.Cms.Web.Common.Controllers;
using Umbraco.Cms.Web.Common.Filters;

namespace jcdcdev.Umbraco.BackOfficeOrganiser;

[IsBackOffice]
[UmbracoUserTimeoutFilter]
[Authorize(Policy = AuthorizationPolicies.BackOfficeAccess)]
[DisableBrowserCache]
[UmbracoRequireHttps]
public class BackOfficeOrganiserController : UmbracoApiController
{
    public IActionResult Organise(string type)
    {
        var organiseType = DetermineOrganiseType(type);
        var attempt = _service.Organise(organiseType);
        if (!attempt.Success)
        {
            return BadRequest(OrganiseResponse.Fail($"Failed to organise {type}"));
        }

        return Ok(OrganiseResponse.Success($"Successfully organised {type}"));
    }

    private static OrganiseType DetermineOrganiseType(string type)
    {
        var organise = type.ToLowerInvariant() switch
        {
            "all" => OrganiseType.All,
            "contenttypes" => OrganiseType.ContentTypes,
            "mediatypes" => OrganiseType.MediaTypes,
            "membertypes" => OrganiseType.MemberTypes,
            "datatypes" => OrganiseType.DataTypes,
            _ => OrganiseType.Unknown
        };
        return organise;
    }

    private readonly IBackOfficeOrganiserService _service;

    public BackOfficeOrganiserController(IBackOfficeOrganiserService service)
    {
        _service = service;
    }
}
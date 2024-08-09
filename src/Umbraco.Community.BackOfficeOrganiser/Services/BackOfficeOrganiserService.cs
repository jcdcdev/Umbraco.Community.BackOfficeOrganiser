using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core;
using Umbraco.Community.BackOfficeOrganiser.Models;
using Umbraco.Community.BackOfficeOrganiser.Organisers.ContentTypes;
using Umbraco.Community.BackOfficeOrganiser.Organisers.DataTypes;
using Umbraco.Community.BackOfficeOrganiser.Organisers.MediaTypes;
using Umbraco.Community.BackOfficeOrganiser.Organisers.MemberTypes;

namespace Umbraco.Community.BackOfficeOrganiser.Services;

public class BackOfficeOrganiserService(
    ILogger<BackOfficeOrganiserService> logger,
    ContentTypeOrganiser contentTypeOrganiser,
    MediaTypeOrganiser mediaTypeOrganiser,
    MemberTypeOrganiser memberTypeOrganiser,
    DataTypeOrganiser dataTypeOrganiser)
    : IBackOfficeOrganiserService
{
    private readonly ILogger _logger = logger;

    public Attempt<OrganiseType> Organise(OrganiseType organise)
    {
        try
        {
            switch (organise)
            {
                case OrganiseType.ContentTypes:
                    OrganiseContentTypes();
                    break;
                case OrganiseType.MediaTypes:
                    OrganiseMediaTypes();
                    break;
                case OrganiseType.MemberTypes:
                    OrganiseMemberTypes();
                    break;
                case OrganiseType.DataTypes:
                    OrganiseDataTypes();
                    break;
                case OrganiseType.Unknown:
                default:
                    throw new ArgumentOutOfRangeException(nameof(organise), organise,
                        "Failed to determine OrganiseType");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "BackOfficeOrganiser: Failed to organise {OrganiseType}", organise);
            return Attempt<OrganiseType>.Fail(ex);
        }

        return Attempt<OrganiseType>.Succeed(organise);
    }

    private void OrganiseDataTypes()
    {
        dataTypeOrganiser.OrganiseAll();
    }

    private void OrganiseMemberTypes()
    {
        memberTypeOrganiser.OrganiseAll();
    }

    private void OrganiseMediaTypes()
    {
        mediaTypeOrganiser.OrganiseAll();
    }

    private void OrganiseContentTypes()
    {
        contentTypeOrganiser.OrganiseAll();
    }
}
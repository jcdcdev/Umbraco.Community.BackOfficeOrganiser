using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core;
using Umbraco.Community.BackOfficeOrganiser.Models;
using Umbraco.Community.BackOfficeOrganiser.Organisers.ContentTypes;
using Umbraco.Community.BackOfficeOrganiser.Organisers.DataTypes;
using Umbraco.Community.BackOfficeOrganiser.Organisers.MediaTypes;
using Umbraco.Community.BackOfficeOrganiser.Organisers.MemberTypes;

namespace Umbraco.Community.BackOfficeOrganiser.Services;

public class BackOfficeOrganiserService : IBackOfficeOrganiserService
{
    private readonly ContentTypeOrganiser _contentTypeOrganiser;
    private readonly DataTypeOrganiser _dataTypeOrganiser;
    private readonly ILogger _logger;
    private readonly MediaTypeOrganiser _mediaTypeOrganiser;
    private readonly MemberTypeOrganiser _memberTypeOrganiser;

    public BackOfficeOrganiserService(
        ILogger<BackOfficeOrganiserService> logger,
        ContentTypeOrganiser contentTypeOrganiser,
        MediaTypeOrganiser mediaTypeOrganiser,
        MemberTypeOrganiser memberTypeOrganiser,
        DataTypeOrganiser dataTypeOrganiser)
    {
        _logger = logger;
        _contentTypeOrganiser = contentTypeOrganiser;
        _mediaTypeOrganiser = mediaTypeOrganiser;
        _memberTypeOrganiser = memberTypeOrganiser;
        _dataTypeOrganiser = dataTypeOrganiser;
    }

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
        _dataTypeOrganiser.OrganiseAll();
    }

    private void OrganiseMemberTypes()
    {
        _memberTypeOrganiser.OrganiseAll();
    }

    private void OrganiseMediaTypes()
    {
        _mediaTypeOrganiser.OrganiseAll();
    }

    private void OrganiseContentTypes()
    {
        _contentTypeOrganiser.OrganiseAll();
    }
}
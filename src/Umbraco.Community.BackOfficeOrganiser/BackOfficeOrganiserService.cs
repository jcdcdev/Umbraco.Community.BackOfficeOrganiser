using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Models;
using Umbraco.Community.BackOfficeOrganiser.Models;
using Umbraco.Community.BackOfficeOrganiser.Organisers;

namespace Umbraco.Community.BackOfficeOrganiser;

public class BackOfficeOrganiserService : IBackOfficeOrganiserService
{
    private readonly IBackOfficeOrganiser<IContentType> _contentTypeOrganiser;
    private readonly IBackOfficeOrganiser<IDataType> _dataTypeOrganiser;
    private readonly ILogger _logger;
    private readonly IBackOfficeOrganiser<IMediaType> _mediaTypeOrganiser;
    private readonly IBackOfficeOrganiser<IMemberType> _memberTypeOrganiser;

    public BackOfficeOrganiserService(
        IBackOfficeOrganiser<IDataType> dataTypeOrganiser,
        IBackOfficeOrganiser<IContentType> contentTypeOrganiser,
        IBackOfficeOrganiser<IMediaType> mediaTypeOrganiser,
        IBackOfficeOrganiser<IMemberType> memberTypeOrganiser,
        ILogger<BackOfficeOrganiserService> logger)
    {
        _dataTypeOrganiser = dataTypeOrganiser;
        _contentTypeOrganiser = contentTypeOrganiser;
        _mediaTypeOrganiser = mediaTypeOrganiser;
        _memberTypeOrganiser = memberTypeOrganiser;
        _logger = logger;
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
                case OrganiseType.All:
                    OrganiseAll();
                    break;
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

    public void OrganiseDataTypes()
    {
        _dataTypeOrganiser.OrganiseType();
    }

    private void OrganiseAll()
    {
        OrganiseContentTypes();
        OrganiseMediaTypes();
        OrganiseMemberTypes();
        OrganiseDataTypes();
    }

    private void OrganiseMemberTypes()
    {
        _memberTypeOrganiser.OrganiseType();
    }

    private void OrganiseMediaTypes()
    {
        _mediaTypeOrganiser.OrganiseType();
    }

    private void OrganiseContentTypes()
    {
        _contentTypeOrganiser.OrganiseType();
    }
}
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

    public async Task<Attempt<OrganiseType>> OrganiseAsync(OrganiseType organise)
    {
        try
        {
            switch (organise)
            {
                case OrganiseType.ContentTypes:
                    await OrganiseContentTypesAsync();
                    break;
                case OrganiseType.MediaTypes:
                    await OrganiseMediaTypesAsync();
                    break;
                case OrganiseType.MemberTypes:
                    await OrganiseMemberTypesAsync();
                    break;
                case OrganiseType.DataTypes:
                    await OrganiseDataTypesAsync();
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

    private async Task OrganiseDataTypesAsync()
    {
        await _dataTypeOrganiser.OrganiseTypeAsync();
    }

    private async Task OrganiseMemberTypesAsync()
    {
        await _memberTypeOrganiser.OrganiseTypeAsync();
    }

    private async Task OrganiseMediaTypesAsync()
    {
        await _mediaTypeOrganiser.OrganiseTypeAsync();
    }

    private async Task OrganiseContentTypesAsync()
    {
        await _contentTypeOrganiser.OrganiseTypeAsync();
    }
}
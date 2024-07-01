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

    public async Task<Attempt<OrganiseType>> OrganiseDataTypesAsync()
    {
        try
        {
            await dataTypeOrganiser.OrganiseTypeAsync();
        }
        catch (Exception ex)
        {
            return Attempt<OrganiseType>.Fail(ex);
        }

        return Attempt<OrganiseType>.Succeed(OrganiseType.DataTypes);
    }

    public async Task<Attempt<OrganiseType>> OrganiseMemberTypesAsync()
    {
        try
        {
            await memberTypeOrganiser.OrganiseTypeAsync();
        }
        catch (Exception ex)
        {
            return Attempt<OrganiseType>.Fail(ex);
        }

        return Attempt<OrganiseType>.Succeed(OrganiseType.MemberTypes);
    }

    public async Task<Attempt<OrganiseType>> OrganiseMediaTypesAsync()
    {
        try
        {
            await mediaTypeOrganiser.OrganiseTypeAsync();
        }
        catch (Exception ex)
        {
            return Attempt<OrganiseType>.Fail(ex);
        }

        return Attempt<OrganiseType>.Succeed(OrganiseType.MediaTypes);
    }

    public async Task<Attempt<OrganiseType>> OrganiseContentTypesAsync()
    {
        try
        {
            await contentTypeOrganiser.OrganiseTypeAsync();
        }
        catch (Exception ex)
        {
            return Attempt<OrganiseType>.Fail(ex);
        }

        return Attempt<OrganiseType>.Succeed(OrganiseType.ContentTypes);
    }
}
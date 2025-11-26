using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core;
using Umbraco.Community.BackOfficeOrganiser.Core.Models;
using Umbraco.Community.BackOfficeOrganiser.Core.OrganiseActions;
using Umbraco.Community.BackOfficeOrganiser.Core.Organisers;
using Umbraco.Community.BackOfficeOrganiser.Core.Services;

namespace Umbraco.Community.BackOfficeOrganiser.Infrastructure;

public class BackOfficeOrganiserService(
    ILogger<BackOfficeOrganiserService> logger,
    ContentTypeOrganiser contentTypeOrganiser,
    MediaTypeOrganiser mediaTypeOrganiser,
    MemberTypeOrganiser memberTypeOrganiser,
    DataTypeOrganiser dataTypeOrganiser)
    : IBackOfficeOrganiserService
{
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
                    throw new ArgumentOutOfRangeException(nameof(organise), organise, "Failed to determine OrganiseType");
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "BackOfficeOrganiser: Failed to organise {OrganiseType}", organise);
            return Attempt<OrganiseType>.Fail(ex);
        }

        return Attempt<OrganiseType>.Succeed(organise);
    }

    public IEnumerable<IOrganiseAction> GetActions(OrganiseType type) => type switch
    {
        OrganiseType.ContentTypes => contentTypeOrganiser.GetOrganiseActions(),
        OrganiseType.MediaTypes => mediaTypeOrganiser.GetOrganiseActions(),
        OrganiseType.MemberTypes => memberTypeOrganiser.GetOrganiseActions(),
        OrganiseType.DataTypes => dataTypeOrganiser.GetOrganiseActions(),
        _ => []
    };

    public async Task<Attempt<OrganiseType>> OrganiseDataTypesAsync()
    {
        try
        {
            await dataTypeOrganiser.OrganiseAllAsync();
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
            await memberTypeOrganiser.OrganiseAllAsync();
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
            await mediaTypeOrganiser.OrganiseAllAsync();
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
            await contentTypeOrganiser.OrganiseAllAsync();
        }
        catch (Exception ex)
        {
            return Attempt<OrganiseType>.Fail(ex);
        }

        return Attempt<OrganiseType>.Succeed(OrganiseType.ContentTypes);
    }
}
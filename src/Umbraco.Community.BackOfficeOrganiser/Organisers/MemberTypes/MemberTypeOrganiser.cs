using jcdcdev.Umbraco.Core.Extensions;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;

namespace Umbraco.Community.BackOfficeOrganiser.Organisers.MemberTypes;

public class MemberTypeOrganiser(
    ILogger<MemberTypeOrganiser> logger,
    IMemberTypeService memberTypeService,
    MemberTypeOrganiseActionCollection organiseActions)
    : BackOfficeOrganiserBase<IMemberType>(logger)
{
    protected override async Task OrganiseAsync()
    {
        var memberTypes = memberTypeService.GetAll().ToList();

        foreach (var memberType in memberTypes)
        {
            await OrganiseTypeAsync(memberType);
        }

        memberTypeService.DeleteAllEmptyContainers();
    }

    public async Task OrganiseTypeAsync(IMemberType memberType)
    {
        var organiser = organiseActions.FirstOrDefault(x => x.CanMove(memberType, memberTypeService));
        if (organiser != null)
        {
            await organiser.MoveAsync(memberType, memberTypeService);
        }
    }
}
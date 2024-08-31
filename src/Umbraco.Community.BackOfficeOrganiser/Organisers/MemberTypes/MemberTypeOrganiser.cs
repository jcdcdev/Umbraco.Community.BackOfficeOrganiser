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
    public override async Task OrganiseAsync(IMemberType item)
    {
        var organiser = organiseActions.FirstOrDefault(x => x.CanMove(item, memberTypeService));
        if (organiser != null)
        {
            await organiser.MoveAsync(item, memberTypeService);
        }
    }

    protected override Task<IEnumerable<IMemberType>> GetAllAsync() => Task.FromResult<IEnumerable<IMemberType>>(memberTypeService.GetAll().ToList());

    protected override void PostOrganiseAll()
    {
        memberTypeService.DeleteAllEmptyContainers();
    }
}
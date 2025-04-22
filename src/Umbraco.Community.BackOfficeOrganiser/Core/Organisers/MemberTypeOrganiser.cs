using jcdcdev.Umbraco.Core.Extensions;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;
using Umbraco.Community.BackOfficeOrganiser.Core.Composing;
using Umbraco.Community.BackOfficeOrganiser.Core.OrganiseActions;

namespace Umbraco.Community.BackOfficeOrganiser.Core.Organisers;

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

    public override IEnumerable<IOrganiseAction> GetOrganiseActions() => organiseActions;

    protected override Task<IEnumerable<IMemberType>> GetAllAsync() => Task.FromResult<IEnumerable<IMemberType>>(memberTypeService.GetAll().ToList());

    protected override Task PostOrganiseAll()
    {
        memberTypeService.DeleteAllEmptyContainers();
        return Task.CompletedTask;
    }
}
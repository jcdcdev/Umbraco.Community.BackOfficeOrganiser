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
    public override void Organise(IMemberType item)
    {
        var organiser = organiseActions.FirstOrDefault(x => x.CanMove(item, memberTypeService));
        organiser?.Move(item, memberTypeService);
    }

    protected override List<IMemberType> GetAll() => memberTypeService.GetAll().ToList();

    protected override void PostOrganiseAll()
    {
        memberTypeService.DeleteAllEmptyContainers();
    }
}
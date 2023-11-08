using jcdcdev.Umbraco.Core.Extensions;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;
using Umbraco.Community.BackOfficeOrganiser.Organisers.ContentTypes;

namespace Umbraco.Community.BackOfficeOrganiser.Organisers.MemberTypes;

public class MemberTypeOrganiser : BackOfficeOrganiserBase<IMemberType>
{
    private readonly IMemberTypeService _memberTypeService;
    private readonly MemberTypeOrganiseActionCollection _organiseActions;

    public MemberTypeOrganiser(
        ILogger<MemberTypeOrganiser> logger,
        IMemberTypeService memberTypeService,
        MemberTypeOrganiseActionCollection organiseActions) : base(logger)
    {
        _memberTypeService = memberTypeService;
        _organiseActions = organiseActions;
    }

    public override void Organise()
    {
        var memberTypes = _memberTypeService.GetAll().ToList();

        foreach (var memberType in memberTypes)
        {
            var organiser = _organiseActions.FirstOrDefault(x => x.CanMove(memberType, _memberTypeService));
            organiser?.Move(memberType, _memberTypeService);
        }

        _memberTypeService.DeleteAllEmptyContainers();
    }
}
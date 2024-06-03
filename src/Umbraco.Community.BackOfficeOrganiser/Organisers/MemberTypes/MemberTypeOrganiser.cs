using jcdcdev.Umbraco.Core.Extensions;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;

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

    protected override async Task OrganiseAsync()
    {
        var memberTypes = _memberTypeService.GetAll().ToList();

        foreach (var memberType in memberTypes)
        {
            await OrganiseTypeAsync(memberType);
        }

        _memberTypeService.DeleteAllEmptyContainers();
    }

    public async Task OrganiseTypeAsync(IMemberType memberType)
    {
        var organiser = _organiseActions.FirstOrDefault(x => x.CanMove(memberType, _memberTypeService));
        if (organiser != null)
        {
            await organiser.MoveAsync(memberType, _memberTypeService);
        }
    }
}
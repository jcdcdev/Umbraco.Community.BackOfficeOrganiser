using jcdcdev.Umbraco.BackOfficeOrganiser.Extensions;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;

namespace jcdcdev.Umbraco.BackOfficeOrganiser.Organisers.MemberTypes;

public class MemberTypeOrganiser : IBackOfficeOrganiser<IMemberType>
{
    private readonly IMemberTypeService _memberTypeService;
    private readonly MemberTypeOrganiseActionCollection _organiseActions;

    public MemberTypeOrganiser(IMemberTypeService memberTypeService, MemberTypeOrganiseActionCollection organiseActions)
    {
        _memberTypeService = memberTypeService;
        _organiseActions = organiseActions;
    }

    public void Organise()
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
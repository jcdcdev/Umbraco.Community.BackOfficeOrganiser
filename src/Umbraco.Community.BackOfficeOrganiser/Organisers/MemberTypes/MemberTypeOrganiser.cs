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

    public override void Organise(IMemberType item)
    {
        var organiser = _organiseActions.FirstOrDefault(x => x.CanMove(item, _memberTypeService));
        organiser?.Move(item, _memberTypeService);
    }

    protected override List<IMemberType> GetAll() => _memberTypeService.GetAll().ToList();

    protected override void PostOrganiseAll()
    {
        _memberTypeService.DeleteAllEmptyContainers();
    }
}
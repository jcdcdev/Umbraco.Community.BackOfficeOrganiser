using StackExchange.Profiling.Internal;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;
using Umbraco.Community.BackOfficeOrganiser.Extensions;

namespace Umbraco.Community.BackOfficeOrganiser.Organisers.MemberTypes;

public class DefaultMemberTypeOrganiseAction : IMemberTypeOrganiseAction
{
    public bool CanMove(IMemberType memberType, IMemberTypeService memberTypeService) => !memberType.IsContainer;

    public void Move(IMemberType memberType, IMemberTypeService memberTypeService)
    {
        var folderId = -1;
        var folderName = string.Empty;

        if (memberType.CompositionIds().Any())
        {
            folderName = "Compositions";
        }
        else if (memberType.IsElement)
        {
            folderName = "Element Types";
        }

        if (!folderName.IsNullOrWhiteSpace())
        {
            folderId = memberTypeService.GetOrCreateFolder(folderName).Id;
        }

        memberTypeService.Move(memberType, folderId);
    }
}
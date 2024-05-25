using jcdcdev.Umbraco.Core.Extensions;
using StackExchange.Profiling.Internal;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;

namespace Umbraco.Community.BackOfficeOrganiser.Organisers.MemberTypes;

public class DefaultMemberTypeOrganiseAction : IMemberTypeOrganiseAction
{
    public bool CanMove(IMemberType memberType, IMemberTypeService memberTypeService) => true;

    public async Task MoveAsync(IMemberType memberType, IMemberTypeService memberTypeService)
    {
        var folderKey = Constants.System.RootKey;
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
            folderKey = memberTypeService.GetOrCreateFolder(folderName).Key;
        }

        await memberTypeService.MoveAsync(memberType.Key, folderKey);
    }
}
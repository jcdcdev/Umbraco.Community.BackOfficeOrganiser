using jcdcdev.Umbraco.Core.Extensions;
using StackExchange.Profiling.Internal;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;
using Umbraco.Community.BackOfficeOrganiser.Core.OrganiseActions;

namespace Umbraco.Community.BackOfficeOrganiser.Infrastructure.OrganiseActions;

public class DefaultContentTypeOrganiseAction : IContentTypeOrganiseAction
{
    public bool CanMove(IContentType contentType, IContentTypeService contentTypeService) => true;

    public async Task MoveAsync(IContentType contentType, IContentTypeService contentTypeService)
    {
        var folderKey = Cms.Core.Constants.System.RootKey;
        var folderName = string.Empty;
        var isComposition = contentTypeService.GetComposedOf(contentType.Id).Any();

        if (contentType.AllowedTemplates?.Any() ?? false)
        {
            folderName = "Pages";
        }
        else if (isComposition)
        {
            folderName = "Compositions";
        }
        else if (contentType.IsElement)
        {
            folderName = "Element Types";
        }

        if (!ExtensionMethods.IsNullOrWhiteSpace(folderName))
        {
            folderKey = contentTypeService.GetOrCreateFolder(folderName).Key;
        }

        await contentTypeService.MoveAsync(contentType.Key, folderKey);
    }

    public string Name => "Default Content Type Organise Action";
    public string Description => "Organises content types into folders based on their allowed templates, compositions and element types.";
}
using jcdcdev.Umbraco.Core.Extensions;
using StackExchange.Profiling.Internal;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;

namespace Umbraco.Community.BackOfficeOrganiser.Organisers.ContentTypes;

public class DefaultContentTypeOrganiseAction : IContentTypeOrganiseAction
{
    public bool CanMove(IContentType contentType, IContentTypeService contentTypeService) => true;

    public void Move(IContentType contentType, IContentTypeService contentTypeService)
    {
        var folderId = -1;
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

        if (!folderName.IsNullOrWhiteSpace())
        {
            folderId = contentTypeService.GetOrCreateFolder(folderName).Id;
        }

        contentTypeService.Move(contentType, folderId);
    }
}
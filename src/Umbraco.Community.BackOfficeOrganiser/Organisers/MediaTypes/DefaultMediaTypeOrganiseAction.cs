using jcdcdev.Umbraco.Core.Extensions;
using StackExchange.Profiling.Internal;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;

namespace Umbraco.Community.BackOfficeOrganiser.Organisers.MediaTypes;

public class DefaultMediaTypeOrganiseAction : IMediaTypeOrganiseAction
{
    public bool CanMove(IMediaType mediaType, IMediaTypeService mediaTypeService) => true;

    public async Task MoveAsync(IMediaType mediaType, IMediaTypeService mediaTypeService)
    {
        var folderKey = Constants.System.RootKey;
        var parentId = Constants.System.Root;
        var folderName = string.Empty;

        if (mediaType.IsInternal())
        {
            var parent = mediaTypeService.GetOrCreateFolder("Internal");
            folderKey = parent.Key;
            folderName = mediaType.Alias switch
            {
                Constants.Conventions.MediaTypes.File => string.Empty,
                Constants.Conventions.MediaTypes.Folder => string.Empty,
                Constants.Conventions.MediaTypes.VideoAlias => "Video",
                Constants.Conventions.MediaTypes.AudioAlias => "Audio",
                Constants.Conventions.MediaTypes.ArticleAlias => "Text File",
                Constants.Conventions.MediaTypes.VectorGraphicsAlias => "Image",
                Constants.Conventions.MediaTypes.Image => "Image",
                _ => folderName
            };
        }

        if (mediaType.IsElement)
        {
            folderName = "Element Types";
        }

        if (!folderName.IsNullOrWhiteSpace())
        {
            folderKey = mediaTypeService.GetOrCreateFolder(folderName, parentId).Key;
        }

        await mediaTypeService.MoveAsync(mediaType.Key, folderKey);
    }
}
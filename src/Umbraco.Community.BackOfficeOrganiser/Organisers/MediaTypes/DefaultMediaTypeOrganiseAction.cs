using StackExchange.Profiling.Internal;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;
using Umbraco.Community.BackOfficeOrganiser.Extensions;

namespace Umbraco.Community.BackOfficeOrganiser.Organisers.MediaTypes;

public class DefaultMediaTypeOrganiseAction : IMediaTypeOrganiseAction
{
    public bool CanMove(IMediaType mediaType, IMediaTypeService mediaTypeService) => !mediaType.IsContainer;

    public void Move(IMediaType mediaType, IMediaTypeService mediaTypeService)
    {
        var folderId = -1;
        var parentId = -1;
        var folderName = string.Empty;

        if (mediaType.IsInternal())
        {
            parentId = mediaTypeService.GetOrCreateFolder("Internal").Id;
            folderId = parentId;
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
            folderId = mediaTypeService.GetOrCreateFolder(folderName, parentId).Id;
        }

        mediaTypeService.Move(mediaType, folderId);
    }
}
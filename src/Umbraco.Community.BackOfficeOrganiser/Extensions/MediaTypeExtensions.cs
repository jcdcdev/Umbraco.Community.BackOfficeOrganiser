using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Models;
using Umbraco.Extensions;

namespace Umbraco.Community.BackOfficeOrganiser.Extensions;

public static class MediaTypeExtensions
{
    private static readonly string[] MediaTypes =
    {
        Constants.Conventions.MediaTypes.File,
        Constants.Conventions.MediaTypes.Folder,
        Constants.Conventions.MediaTypes.Image,
        Constants.Conventions.MediaTypes.Video,
        Constants.Conventions.MediaTypes.Audio,
        Constants.Conventions.MediaTypes.Article,
        Constants.Conventions.MediaTypes.VectorGraphics,
        Constants.Conventions.MediaTypes.VideoAlias,
        Constants.Conventions.MediaTypes.AudioAlias,
        Constants.Conventions.MediaTypes.ArticleAlias,
        Constants.Conventions.MediaTypes.VectorGraphicsAlias
    };

    public static bool IsInternal(this IMediaType mediaType) => MediaTypes.InvariantContains(mediaType.Alias);
}
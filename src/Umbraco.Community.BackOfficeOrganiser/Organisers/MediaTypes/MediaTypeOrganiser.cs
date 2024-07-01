using jcdcdev.Umbraco.Core.Extensions;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;

namespace Umbraco.Community.BackOfficeOrganiser.Organisers.MediaTypes;

public class MediaTypeOrganiser(
    ILogger<MediaTypeOrganiser> logger,
    IMediaTypeService mediaTypeService,
    MediaTypeOrganiseActionCollection organiseActions)
    : BackOfficeOrganiserBase<IMediaType>(logger)
{
    protected override async Task OrganiseAsync()
    {
        var mediaTypes = mediaTypeService.GetAll().ToList();

        foreach (var mediaType in mediaTypes)
        {
            await OrganiseTypeAsync(mediaType);
        }

        mediaTypeService.DeleteAllEmptyContainers();
    }

    public async Task OrganiseTypeAsync(IMediaType mediaType)
    {
        var organiser = organiseActions.FirstOrDefault(x => x.CanMove(mediaType, mediaTypeService));
        if (organiser != null)
        {
            await organiser.MoveAsync(mediaType, mediaTypeService);
        }
    }
}
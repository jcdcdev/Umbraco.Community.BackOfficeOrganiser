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
    protected override Task<IEnumerable<IMediaType>> GetAllAsync() => Task.FromResult(mediaTypeService.GetAll());

    public override async Task OrganiseAsync(IMediaType mediaType)
    {
        var organiser = organiseActions.FirstOrDefault(x => x.CanMove(mediaType, mediaTypeService));
        if (organiser != null)
        {
            await organiser.MoveAsync(mediaType, mediaTypeService);
        }
    }
    
    protected override void PostOrganiseAll()
    {
        mediaTypeService.DeleteAllEmptyContainers();
    }
}
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
    public override void Organise(IMediaType mediaType)
    {
        var organiser = organiseActions.FirstOrDefault(x => x.CanMove(mediaType, mediaTypeService));
        organiser?.Move(mediaType, mediaTypeService);
    }

    protected override List<IMediaType> GetAll() => mediaTypeService.GetAll().ToList();
    
    protected override void PostOrganiseAll()
    {
        mediaTypeService.DeleteAllEmptyContainers();
    }
}
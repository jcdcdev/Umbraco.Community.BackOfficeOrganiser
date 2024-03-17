using jcdcdev.Umbraco.Core.Extensions;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;

namespace Umbraco.Community.BackOfficeOrganiser.Organisers.MediaTypes;

public class MediaTypeOrganiser : BackOfficeOrganiserBase<IMediaType>
{
    private readonly IMediaTypeService _mediaTypeService;
    private readonly MediaTypeOrganiseActionCollection _organiseActions;

    public MediaTypeOrganiser(
        ILogger<MediaTypeOrganiser> logger,
        IMediaTypeService mediaTypeService,
        MediaTypeOrganiseActionCollection organiseActions) : base(logger)
    {
        _mediaTypeService = mediaTypeService;
        _organiseActions = organiseActions;
    }

    public override void Organise()
    {
        var mediaTypes = _mediaTypeService.GetAll().ToList();

        foreach (var mediaType in mediaTypes)
        {
            var organiser = _organiseActions.FirstOrDefault(x => x.CanMove(mediaType, _mediaTypeService));
            organiser?.Move(mediaType, _mediaTypeService);
        }

        _mediaTypeService.DeleteAllEmptyContainers();
    }
}
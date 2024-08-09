using jcdcdev.Umbraco.Core.Extensions;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;

namespace Umbraco.Community.BackOfficeOrganiser.Organisers.ContentTypes;

public class ContentTypeOrganiser(
    ILogger<ContentTypeOrganiser> logger,
    IContentTypeService contentTypeService,
    ContentTypeOrganiseActionCollection organiseActions)
    : BackOfficeOrganiserBase<IContentType>(logger)
{
    protected override List<IContentType> GetAll() => contentTypeService.GetAll().ToList();

    public override void Organise(IContentType contentType)
    {
        var organiser = organiseActions.FirstOrDefault(x => x.CanMove(contentType, contentTypeService));
        organiser?.Move(contentType, contentTypeService);
    }

    protected override void PostOrganiseAll()
    {
        contentTypeService.DeleteAllEmptyContainers();
    }
}
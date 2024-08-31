using System.Collections;
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
    protected override Task<IEnumerable<IContentType>> GetAllAsync() => Task.FromResult(contentTypeService.GetAll());

    public override async Task OrganiseAsync(IContentType contentType)
    {
        var organiser = organiseActions.FirstOrDefault(x => x.CanMove(contentType, contentTypeService));
        if (organiser != null)
        {
            await organiser.MoveAsync(contentType, contentTypeService);
        }
    }

    protected override void PostOrganiseAll()
    {
        contentTypeService.DeleteAllEmptyContainers();
    }
}
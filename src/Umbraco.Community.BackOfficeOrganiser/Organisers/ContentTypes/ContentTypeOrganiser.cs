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
    protected override async Task OrganiseAsync()
    {
        var contentTypes = contentTypeService.GetAll().ToList();
        foreach (var contentType in contentTypes)
        {
            await OrganiseTypeAsync(contentType);
        }

        contentTypeService.DeleteAllEmptyContainers();
    }

    public async Task OrganiseTypeAsync(IContentType contentType)
    {
        var organiser = organiseActions.FirstOrDefault(x => x.CanMove(contentType, contentTypeService));
        if (organiser != null)
        {
            await organiser.MoveAsync(contentType, contentTypeService);
        }
    }
}
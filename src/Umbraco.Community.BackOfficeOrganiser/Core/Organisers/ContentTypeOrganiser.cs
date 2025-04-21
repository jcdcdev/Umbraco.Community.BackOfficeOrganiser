using jcdcdev.Umbraco.Core.Extensions;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;
using Umbraco.Community.BackOfficeOrganiser.Core.Composing;
using Umbraco.Community.BackOfficeOrganiser.Core.OrganiseActions;

namespace Umbraco.Community.BackOfficeOrganiser.Core.Organisers;

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

    public override IEnumerable<IOrganiseAction> GetOrganiseActions() => organiseActions;

    protected override Task PostOrganiseAll()
    {
        contentTypeService.DeleteAllEmptyContainers();
        return Task.CompletedTask;
    }
}
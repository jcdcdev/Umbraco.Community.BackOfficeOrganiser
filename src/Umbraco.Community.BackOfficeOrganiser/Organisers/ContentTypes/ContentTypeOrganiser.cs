using jcdcdev.Umbraco.Core.Extensions;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;

namespace Umbraco.Community.BackOfficeOrganiser.Organisers.ContentTypes;

public class ContentTypeOrganiser : BackOfficeOrganiserBase<IContentType>
{
    private readonly IContentTypeService _contentTypeService;
    private readonly ContentTypeOrganiseActionCollection _organiseActions;

    public ContentTypeOrganiser(
        ILogger<ContentTypeOrganiser> logger,
        IContentTypeService contentTypeService,
        ContentTypeOrganiseActionCollection organiseActions) : base(logger)
    {
        _contentTypeService = contentTypeService;
        _organiseActions = organiseActions;
    }

    protected override async Task OrganiseAsync()
    {
        var contentTypes = _contentTypeService.GetAll().ToList();
        foreach (var contentType in contentTypes)
        {
            var organiser = _organiseActions.FirstOrDefault(x => x.CanMove(contentType, _contentTypeService));
            if (organiser != null)
            {
                await organiser.MoveAsync(contentType, _contentTypeService);
            }
        }

        _contentTypeService.DeleteAllEmptyContainers();
    }
}
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;
using Umbraco.Community.BackOfficeOrganiser.Extensions;

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

    public override void Organise()
    {
        var contentTypes = _contentTypeService.GetAll().ToList();

        foreach (var contentType in contentTypes)
        {
            var organiser = _organiseActions.FirstOrDefault(x => x.CanMove(contentType, _contentTypeService));
            organiser?.Move(contentType, _contentTypeService);
        }

        _contentTypeService.DeleteAllEmptyContainers();
    }
}
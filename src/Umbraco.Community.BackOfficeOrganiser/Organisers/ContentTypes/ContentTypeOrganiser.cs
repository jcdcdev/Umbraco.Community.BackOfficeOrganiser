using Umbraco.Community.BackOfficeOrganiser.Extensions;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;

namespace Umbraco.Community.BackOfficeOrganiser.Organisers.ContentTypes;

public class ContentTypeOrganiser : IBackOfficeOrganiser<IContentType>
{
    private readonly IContentTypeService _contentTypeService;
    private readonly ContentTypeOrganiseActionCollection _organiseActions;

    public ContentTypeOrganiser(IContentTypeService contentTypeService, ContentTypeOrganiseActionCollection organiseActions)
    {
        _contentTypeService = contentTypeService;
        _organiseActions = organiseActions;
    }

    public void Organise()
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
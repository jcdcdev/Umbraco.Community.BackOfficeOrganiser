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

    protected override List<IContentType> GetAll() => _contentTypeService.GetAll().ToList();

    public override void Organise(IContentType contentType)
    {
        var organiser = _organiseActions.FirstOrDefault(x => x.CanMove(contentType, _contentTypeService));
        organiser?.Move(contentType, _contentTypeService);
    }

    protected override void PostOrganiseAll()
    {
        _contentTypeService.DeleteAllEmptyContainers();
    }
}
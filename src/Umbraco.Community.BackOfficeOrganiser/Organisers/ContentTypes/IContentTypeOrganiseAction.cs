using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;

namespace Umbraco.Community.BackOfficeOrganiser.Organisers.ContentTypes;

public interface IContentTypeOrganiseAction
{
    public bool CanMove(IContentType contentType, IContentTypeService contentTypeService);
    public void Move(IContentType contentType, IContentTypeService contentTypeService);
}
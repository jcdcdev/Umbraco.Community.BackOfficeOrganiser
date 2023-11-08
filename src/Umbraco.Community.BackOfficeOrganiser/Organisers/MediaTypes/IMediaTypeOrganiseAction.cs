using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;

namespace Umbraco.Community.BackOfficeOrganiser.Organisers.MediaTypes;

public interface IMediaTypeOrganiseAction
{
    public bool CanMove(IMediaType mediaType, IMediaTypeService mediaTypeService);
    public void Move(IMediaType mediaType, IMediaTypeService mediaTypeService);
}
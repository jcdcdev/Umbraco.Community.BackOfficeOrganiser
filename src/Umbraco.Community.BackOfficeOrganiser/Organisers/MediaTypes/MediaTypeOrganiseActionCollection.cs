using Umbraco.Cms.Core.Composing;

namespace Umbraco.Community.BackOfficeOrganiser.Organisers.MediaTypes;

public class MediaTypeOrganiseActionCollection : BuilderCollectionBase<IMediaTypeOrganiseAction>
{
    public MediaTypeOrganiseActionCollection(Func<IEnumerable<IMediaTypeOrganiseAction>> items)
        : base(items)
    {
    }
}
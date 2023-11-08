using Umbraco.Cms.Core.Composing;

namespace Umbraco.Community.BackOfficeOrganiser.Organisers.ContentTypes;

public class ContentTypeOrganiseActionCollection : BuilderCollectionBase<IContentTypeOrganiseAction>
{
    public ContentTypeOrganiseActionCollection(Func<IEnumerable<IContentTypeOrganiseAction>> items)
        : base(items)
    {
    }
}
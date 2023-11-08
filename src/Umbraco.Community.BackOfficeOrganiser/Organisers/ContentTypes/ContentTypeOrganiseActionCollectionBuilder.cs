using Umbraco.Cms.Core.Composing;

namespace Umbraco.Community.BackOfficeOrganiser.Organisers.ContentTypes;

public class ContentTypeOrganiseActionCollectionBuilder : OrderedCollectionBuilderBase<ContentTypeOrganiseActionCollectionBuilder, ContentTypeOrganiseActionCollection, IContentTypeOrganiseAction>
{
    protected override ContentTypeOrganiseActionCollectionBuilder This => this;
}
using Umbraco.Cms.Core.Composing;

namespace jcdcdev.Umbraco.BackOfficeOrganiser.Organisers.ContentTypes;

public class ContentTypeOrganiseActionCollectionBuilder : OrderedCollectionBuilderBase<ContentTypeOrganiseActionCollectionBuilder, ContentTypeOrganiseActionCollection, IContentTypeOrganiseAction>
{
    protected override ContentTypeOrganiseActionCollectionBuilder This => this;
}
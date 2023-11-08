using Umbraco.Cms.Core.Composing;

namespace Umbraco.Community.BackOfficeOrganiser.Organisers.MediaTypes;

public class MediaTypeOrganiseActionCollectionBuilder : OrderedCollectionBuilderBase<MediaTypeOrganiseActionCollectionBuilder, MediaTypeOrganiseActionCollection, IMediaTypeOrganiseAction>
{
    protected override MediaTypeOrganiseActionCollectionBuilder This => this;
}
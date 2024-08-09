using Umbraco.Cms.Core.Composing;

namespace Umbraco.Community.BackOfficeOrganiser.Organisers.ContentTypes;

public class ContentTypeOrganiseActionCollection(Func<IEnumerable<IContentTypeOrganiseAction>> items) : BuilderCollectionBase<IContentTypeOrganiseAction>(items);
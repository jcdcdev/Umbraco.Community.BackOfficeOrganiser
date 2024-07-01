using Umbraco.Cms.Core.Composing;

namespace Umbraco.Community.BackOfficeOrganiser.Organisers.MediaTypes;

public class MediaTypeOrganiseActionCollection(Func<IEnumerable<IMediaTypeOrganiseAction>> items) : BuilderCollectionBase<IMediaTypeOrganiseAction>(items);
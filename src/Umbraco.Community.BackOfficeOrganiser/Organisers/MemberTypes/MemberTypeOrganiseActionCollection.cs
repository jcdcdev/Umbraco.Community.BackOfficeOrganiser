using Umbraco.Cms.Core.Composing;

namespace Umbraco.Community.BackOfficeOrganiser.Organisers.MemberTypes;

public class MemberTypeOrganiseActionCollection(Func<IEnumerable<IMemberTypeOrganiseAction>> items) : BuilderCollectionBase<IMemberTypeOrganiseAction>(items);
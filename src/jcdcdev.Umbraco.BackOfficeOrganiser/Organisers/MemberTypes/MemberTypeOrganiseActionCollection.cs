using Umbraco.Cms.Core.Composing;

namespace jcdcdev.Umbraco.BackOfficeOrganiser.Organisers.MemberTypes;

public class MemberTypeOrganiseActionCollection : BuilderCollectionBase<IMemberTypeOrganiseAction>
{
    public MemberTypeOrganiseActionCollection(Func<IEnumerable<IMemberTypeOrganiseAction>> items)
        : base(items)
    {
    }
}
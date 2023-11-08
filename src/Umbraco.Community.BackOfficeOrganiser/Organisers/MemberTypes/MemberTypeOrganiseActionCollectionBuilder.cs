using Umbraco.Cms.Core.Composing;

namespace Umbraco.Community.BackOfficeOrganiser.Organisers.MemberTypes;

public class MemberTypeOrganiseActionCollectionBuilder : OrderedCollectionBuilderBase<MemberTypeOrganiseActionCollectionBuilder, MemberTypeOrganiseActionCollection, IMemberTypeOrganiseAction>
{
    protected override MemberTypeOrganiseActionCollectionBuilder This => this;
}
using Umbraco.Cms.Core.Composing;
using Umbraco.Community.BackOfficeOrganiser.Composing;

namespace TestSite.Thirteen;

public class Composer : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        builder.DataTypeOrganiseActions().Insert<ExampleDataTypeOrganiseAction>();
    }
}
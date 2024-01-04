using Umbraco.Cms.Core.Composing;
using Umbraco.Community.BackOfficeOrganiser;

namespace TestSite.Eleven;

public class Composer : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        builder.DataTypeOrganiseActions().Insert<ExampleDataTypeOrganiseAction>();
    }
}
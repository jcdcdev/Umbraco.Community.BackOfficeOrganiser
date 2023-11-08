using Umbraco.Cms.Core.Composing;
using Umbraco.Community.BackOfficeOrganiser.Extensions;

namespace Umbraco.Community.BackOfficeOrganiser.TestSite;

public class Composer : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        builder.DataTypeOrganiseActions().Insert<ExampleDataTypeOrganiseAction>();
    }
}
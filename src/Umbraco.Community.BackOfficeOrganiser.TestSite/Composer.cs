using Umbraco.Community.BackOfficeOrganiser.Extensions;
using Umbraco.Cms.Core.Composing;

namespace Umbraco.Community.BackOfficeOrganiser.TestSite;

public class Composer : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        builder.DataTypeOrganiseActions().Insert<ExampleDataTypeOrganiseAction>();
    }
}
using Umbraco.Cms.Core.Composing;

namespace jcdcdev.Umbraco.BackOfficeOrganiser.Organisers.DataTypes;

public class DataTypeOrganiseActionCollection : BuilderCollectionBase<IDataTypeOrganiseAction>
{
    public DataTypeOrganiseActionCollection(Func<IEnumerable<IDataTypeOrganiseAction>> items)
        : base(items)
    {
    }
}
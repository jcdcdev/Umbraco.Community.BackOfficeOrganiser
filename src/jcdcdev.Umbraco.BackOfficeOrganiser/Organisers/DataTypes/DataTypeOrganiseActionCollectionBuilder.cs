using Umbraco.Cms.Core.Composing;

namespace jcdcdev.Umbraco.BackOfficeOrganiser.Organisers.DataTypes;

public class DataTypeOrganiseActionCollectionBuilder : OrderedCollectionBuilderBase<DataTypeOrganiseActionCollectionBuilder, DataTypeOrganiseActionCollection, IDataTypeOrganiseAction>
{
    protected override DataTypeOrganiseActionCollectionBuilder This => this;
}
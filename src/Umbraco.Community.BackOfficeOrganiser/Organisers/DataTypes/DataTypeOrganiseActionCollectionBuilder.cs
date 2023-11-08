using Umbraco.Cms.Core.Composing;

namespace Umbraco.Community.BackOfficeOrganiser.Organisers.DataTypes;

public class DataTypeOrganiseActionCollectionBuilder : OrderedCollectionBuilderBase<DataTypeOrganiseActionCollectionBuilder, DataTypeOrganiseActionCollection, IDataTypeOrganiseAction>
{
    protected override DataTypeOrganiseActionCollectionBuilder This => this;
}
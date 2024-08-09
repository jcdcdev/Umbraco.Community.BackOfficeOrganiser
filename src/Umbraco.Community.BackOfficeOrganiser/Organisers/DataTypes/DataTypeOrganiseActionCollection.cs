using Umbraco.Cms.Core.Composing;

namespace Umbraco.Community.BackOfficeOrganiser.Organisers.DataTypes;

public class DataTypeOrganiseActionCollection(Func<IEnumerable<IDataTypeOrganiseAction>> items) : BuilderCollectionBase<IDataTypeOrganiseAction>(items);
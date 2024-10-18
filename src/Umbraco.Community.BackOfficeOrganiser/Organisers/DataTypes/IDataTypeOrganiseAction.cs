using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;

namespace Umbraco.Community.BackOfficeOrganiser.Organisers.DataTypes;

public interface IDataTypeOrganiseAction
{
    public bool CanMove(IDataType dataType, IDataTypeService dataTypeService, IDataTypeContainerService dataTypeContainerService);
    public Task MoveAsync(IDataType dataType, IDataTypeService dataTypeService, IDataTypeContainerService dataTypeContainerService);
}
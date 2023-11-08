using Umbraco.Community.BackOfficeOrganiser.Extensions;
using Umbraco.Community.BackOfficeOrganiser.Organisers.DataTypes;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;

namespace Umbraco.Community.BackOfficeOrganiser.TestSite;

public class ExampleDataTypeOrganiseAction : IDataTypeOrganiseAction
{
    public bool CanMove(IDataType dataType, IDataTypeService dataTypeService) =>
        dataType.EditorAlias.InvariantStartsWith("Umbraco.Community");

    public void Move(IDataType dataType, IDataTypeService dataTypeService)
    {
        var folder = dataTypeService.GetOrCreateFolder("Community", -1);
        dataTypeService.Move(dataType, folder.Id);
    }
}
using jcdcdev.Umbraco.Core.Extensions;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;
using Umbraco.Community.BackOfficeOrganiser.Organisers.DataTypes;

namespace Umbraco.Community.BackOfficeOrganiser.TestSite;

public class ExampleDataTypeOrganiseAction : IDataTypeOrganiseAction
{
    public bool CanMove(IDataType dataType, IDataTypeService dataTypeService) => dataType.EditorAlias.InvariantStartsWith("Umbraco.Community");

    public void Move(IDataType dataType, IDataTypeService dataTypeService)
    {
        var folder = dataTypeService.GetOrCreateFolder("Community");
        dataTypeService.Move(dataType, folder.Id);
    }
}
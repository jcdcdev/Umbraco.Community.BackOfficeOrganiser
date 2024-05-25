using jcdcdev.Umbraco.Core.Extensions;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;
using Umbraco.Community.BackOfficeOrganiser.Organisers.DataTypes;

namespace TestSite.Fourteen;

public class ExampleDataTypeOrganiseAction : IDataTypeOrganiseAction
{
    public bool CanMove(IDataType dataType, IDataTypeService dataTypeService) => dataType.EditorAlias.InvariantContains("Media");

    public async Task MoveAsync(IDataType dataType, IDataTypeService dataTypeService)
    {
        var folder = dataTypeService.GetOrCreateFolder("📂 - Media");
        await dataTypeService.MoveAsync(dataType, folder.Key, Constants.Security.SuperUserKey);
    }
}
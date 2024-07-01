using jcdcdev.Umbraco.Core.Extensions;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;

namespace Umbraco.Community.BackOfficeOrganiser.Organisers.DataTypes;

public class DataTypeOrganiser(
    ILogger<DataTypeOrganiser> logger,
    IDataTypeService dataTypeService,
    DataTypeOrganiseActionCollection organiseActions)
    : BackOfficeOrganiserBase<IDataType>(logger)
{
    protected override async Task OrganiseAsync()
    {
        var dataTypes = await dataTypeService.GetAllAsync();
        foreach (var dataType in dataTypes)
        {
            await OrganiseAsync(dataType);
        }

        dataTypeService.DeleteAllEmptyContainers();
    }

    public async Task OrganiseAsync(IDataType dataType)
    {
        var organiser = organiseActions.FirstOrDefault(x => x.CanMove(dataType, dataTypeService));
        if (organiser != null)
        {
            await organiser.MoveAsync(dataType, dataTypeService);
        }
    }
}
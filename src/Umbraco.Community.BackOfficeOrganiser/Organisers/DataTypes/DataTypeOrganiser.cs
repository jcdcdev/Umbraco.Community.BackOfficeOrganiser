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
    public override async Task OrganiseAsync(IDataType dataType)
    {
        var organiser = organiseActions.FirstOrDefault(x => x.CanMove(dataType, dataTypeService));
        if (organiser != null)
        {
            await organiser.MoveAsync(dataType, dataTypeService);
        }
    }

    protected override async Task<IEnumerable<IDataType>> GetAllAsync() => await dataTypeService.GetAllAsync();

    protected override void PostOrganiseAll()
    {
        dataTypeService.DeleteAllEmptyContainers();
    }
}
using jcdcdev.Umbraco.Core.Extensions;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;

namespace Umbraco.Community.BackOfficeOrganiser.Organisers.DataTypes;

public class DataTypeOrganiser : BackOfficeOrganiserBase<IDataType>
{
    private readonly IDataTypeService _dataTypeService;
    private readonly DataTypeOrganiseActionCollection _organiseActions;

    public DataTypeOrganiser(
        ILogger<DataTypeOrganiser> logger,
        IDataTypeService dataTypeService,
        DataTypeOrganiseActionCollection organiseActions) : base(logger)
    {
        _dataTypeService = dataTypeService;
        _organiseActions = organiseActions;
    }

    protected override async Task OrganiseAsync()
    {
        var dataTypes = await _dataTypeService.GetAllAsync();
        foreach (var dataType in dataTypes)
        {
            var organiser = _organiseActions.FirstOrDefault(x => x.CanMove(dataType, _dataTypeService));
            if (organiser != null)
            {
                await organiser.MoveAsync(dataType, _dataTypeService);
            }
        }

        _dataTypeService.DeleteAllEmptyContainers();
    }
}
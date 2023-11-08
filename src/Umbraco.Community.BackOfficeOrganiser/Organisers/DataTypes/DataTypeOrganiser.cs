using jcdcdev.Umbraco.Core.Extensions;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;
using Umbraco.Community.BackOfficeOrganiser.Organisers.ContentTypes;

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

    public override void Organise()
    {
        var dataTypes = _dataTypeService.GetAll().ToList();

        foreach (var dataType in dataTypes)
        {
            var organiser = _organiseActions.FirstOrDefault(x => x.CanMove(dataType, _dataTypeService));
            organiser?.Move(dataType, _dataTypeService);
        }

        _dataTypeService.DeleteAllEmptyContainers();
    }
}
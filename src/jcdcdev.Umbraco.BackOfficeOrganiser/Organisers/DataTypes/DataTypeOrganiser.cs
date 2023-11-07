using jcdcdev.Umbraco.BackOfficeOrganiser.Extensions;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;

namespace jcdcdev.Umbraco.BackOfficeOrganiser.Organisers.DataTypes;

public class DataTypeOrganiser : IBackOfficeOrganiser<IDataType>
{
    private readonly IDataTypeService _dataTypeService;
    private readonly DataTypeOrganiseActionCollection _organiseActions;

    public DataTypeOrganiser(IDataTypeService dataTypeService, DataTypeOrganiseActionCollection organiseActions)
    {
        _dataTypeService = dataTypeService;
        _organiseActions = organiseActions;
    }

    public void Organise()
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
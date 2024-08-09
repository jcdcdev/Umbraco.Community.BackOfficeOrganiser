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
    public override void Organise(IDataType dataType)
    {
        var organiser = organiseActions.FirstOrDefault(x => x.CanMove(dataType, dataTypeService));
        organiser?.Move(dataType, dataTypeService);
    }

    protected override List<IDataType> GetAll() => dataTypeService.GetAll().ToList();
    
    protected override void PostOrganiseAll()
    {
        dataTypeService.DeleteAllEmptyContainers();
    }
}
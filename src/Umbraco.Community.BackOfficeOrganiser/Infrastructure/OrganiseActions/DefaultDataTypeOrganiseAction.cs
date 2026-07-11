using jcdcdev.Umbraco.Core.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;
using Umbraco.Community.BackOfficeOrganiser.Core.Models;
using Umbraco.Community.BackOfficeOrganiser.Core.OrganiseActions;
using Umbraco.Extensions;

namespace Umbraco.Community.BackOfficeOrganiser.Infrastructure.OrganiseActions;

public class DefaultDataTypeOrganiseAction(IOptions<BackOfficeOrganiserOptions> options, ILogger<DefaultDataTypeOrganiseAction> logger) : IDataTypeOrganiseAction
{
    private readonly ILogger _logger = logger;
    private readonly BackOfficeOrganiserOptions _options = options.Value;

    public bool CanMove(IDataType dataType, IDataTypeService dataTypeService, IDataTypeContainerService dataTypeContainerService) => true;

    public async Task MoveAsync(IDataType dataType, IDataTypeService dataTypeService, IDataTypeContainerService dataTypeContainerService)
    {
        string internalFolder;
        if (dataType.IsInternalUmbracoEditor())
        {
            internalFolder = _options.DataTypes.InternalFolderName;
        }
        else if (dataType.IsUmbracoEditor())
        {
            internalFolder = _options.DataTypes.CustomFolderName;
        }
        else
        {
            internalFolder = _options.DataTypes.ThirdPartyFolderName;
        }

        var parentFolder = await dataTypeContainerService.GetOrCreateFolderAsync(internalFolder);
        var folder = GetFolderName(dataType);
        if (folder.IsNullOrWhiteSpace())
        {
            _logger.LogWarning("Failed to determine folder name. {DataType} will be considered Custom", dataType.Name);
            await dataTypeService.MoveAsync(dataType, parentFolder.Key, Cms.Core.Constants.Security.SuperUserKey);
            return;
        }

        var dataTypeFolder = await dataTypeContainerService.GetOrCreateFolderAsync(folder, parentFolder.Id, parentFolder.Key);
        await dataTypeService.MoveAsync(dataType, dataTypeFolder.Key, Cms.Core.Constants.Security.SuperUserKey);
    }

    private static string ResolveDataTypeFolderName(IDataType dataType)
    {
        var segments = dataType.EditorAlias.Split(".").SkipLast(1);
        return string.Join(".", segments);
    }

    private static string GetFolderName(IDataType dataType)
    {
        var folder = dataType.EditorAlias switch
        {
            Cms.Core.Constants.PropertyEditors.Aliases.BlockList => "Block List",
            Cms.Core.Constants.PropertyEditors.Aliases.SingleBlock => "Block List",

            Cms.Core.Constants.PropertyEditors.Aliases.NestedContent => "Nested Content",

            Cms.Core.Constants.PropertyEditors.Aliases.BlockGrid => "Grid",
            Cms.Core.Constants.PropertyEditors.Aliases.Grid => "Grid",

            Cms.Core.Constants.PropertyEditors.Aliases.ImageCropper => "Image Cropper",

            Cms.Core.Constants.PropertyEditors.Aliases.ListView => "List View",

            Cms.Core.Constants.PropertyEditors.Aliases.Boolean => "Checkbox",
            Cms.Core.Constants.PropertyEditors.Aliases.UploadField => "Upload",

            Cms.Core.Constants.PropertyEditors.Aliases.Tags => "Tags",

            Cms.Core.Constants.PropertyEditors.Aliases.CheckBoxList => "List",
            Cms.Core.Constants.PropertyEditors.Aliases.DropDownListFlexible => "List",
            Cms.Core.Constants.PropertyEditors.Aliases.RadioButtonList => "List",

            Cms.Core.Constants.PropertyEditors.Aliases.ColorPickerEyeDropper => "Picker",
            Cms.Core.Constants.PropertyEditors.Aliases.ColorPicker => "Picker",
            Cms.Core.Constants.PropertyEditors.Aliases.ContentPicker => "Picker",
            Cms.Core.Constants.PropertyEditors.Aliases.EntityDataPicker => "Picker",
            Cms.Core.Constants.PropertyEditors.Aliases.MultipleMediaPicker => "Picker",
            Cms.Core.Constants.PropertyEditors.Aliases.MemberPicker => "Picker",
            Cms.Core.Constants.PropertyEditors.Aliases.MemberGroupPicker => "Picker",
            Cms.Core.Constants.PropertyEditors.Aliases.MultiNodeTreePicker => "Picker",
            Cms.Core.Constants.PropertyEditors.Aliases.MediaPicker3 => "Picker",
            Cms.Core.Constants.PropertyEditors.Aliases.UserPicker => "Picker",
            Cms.Core.Constants.PropertyEditors.Aliases.MultiUrlPicker => "Picker",
            Cms.Core.Constants.PropertyEditors.Aliases.PickerRelations => "Picker",

            Cms.Core.Constants.PropertyEditors.Aliases.Decimal => "Number",
            Cms.Core.Constants.PropertyEditors.Aliases.Slider => "Number",
            Cms.Core.Constants.PropertyEditors.Aliases.Integer => "Number",

            Cms.Core.Constants.PropertyEditors.Aliases.DateTime => "Date",
            Cms.Core.Constants.PropertyEditors.Aliases.DateOnly => "Date",
            Cms.Core.Constants.PropertyEditors.Aliases.DateTimeUnspecified => "Date",
            Cms.Core.Constants.PropertyEditors.Aliases.DateTimeWithTimeZone => "Date",
            Cms.Core.Constants.PropertyEditors.Aliases.PlainDateTime => "Date",

            Cms.Core.Constants.PropertyEditors.Aliases.MultipleTextstring => "Text",
            Cms.Core.Constants.PropertyEditors.Aliases.TextBox => "Text",
            Cms.Core.Constants.PropertyEditors.Aliases.TextArea => "Text",
            Cms.Core.Constants.PropertyEditors.Aliases.EmailAddress => "Text",
            Cms.Core.Constants.PropertyEditors.Aliases.Label => "Text",
            
            Cms.Core.Constants.PropertyEditors.Aliases.RichText => "Rich Text",
            Cms.Core.Constants.PropertyEditors.Aliases.MarkdownEditor => "Rich Text",

            _ => ResolveDataTypeFolderName(dataType)
        };
        return folder;
    }

    public string Name => "Default Data Type Organise Action";
    public string Description => "Organises data types into folders based on their editor alias";
}
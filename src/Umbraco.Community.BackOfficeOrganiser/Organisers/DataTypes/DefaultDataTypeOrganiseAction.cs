using jcdcdev.Umbraco.Core.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;
using Umbraco.Community.BackOfficeOrganiser.Models;
using Umbraco.Extensions;

namespace Umbraco.Community.BackOfficeOrganiser.Organisers.DataTypes;

public class DefaultDataTypeOrganiseAction : IDataTypeOrganiseAction
{
    private readonly ILogger _logger;
    private readonly BackOfficeOrganiserOptions _options;

    public DefaultDataTypeOrganiseAction(IOptions<BackOfficeOrganiserOptions> options, ILogger<DefaultDataTypeOrganiseAction> logger)
    {
        _logger = logger;
        _options = options.Value;
    }

    public bool CanMove(IDataType dataType, IDataTypeService dataTypeService) => true;

    public void Move(IDataType dataType, IDataTypeService dataTypeService)
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

        var parentFolder = dataTypeService.GetOrCreateFolder(internalFolder);
        var folder = GetFolderName(dataType);
        if (folder.IsNullOrWhiteSpace())
        {
            _logger.LogWarning("Failed to determine folder name. {DataType} will be considered Custom", dataType.Name);
            dataTypeService.Move(dataType, parentFolder.Id);
            return;
        }

        var dataTypeFolder = dataTypeService.GetOrCreateFolder(folder, parentFolder.Id);
        dataTypeService.Move(dataType, dataTypeFolder.Id);
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
            Constants.PropertyEditors.Aliases.BlockList => "Block List",

            Constants.PropertyEditors.Aliases.NestedContent => "Nested Content",
            
            Constants.PropertyEditors.Aliases.BlockGrid => "Grid",
            Constants.PropertyEditors.Aliases.Grid => "Grid",
            
            Constants.PropertyEditors.Aliases.ImageCropper => "Image Cropper",

            Constants.PropertyEditors.Aliases.ListView => "List View",

            Constants.PropertyEditors.Aliases.Boolean => "Checkbox",
            Constants.PropertyEditors.Aliases.UploadField => "Upload",

            Constants.PropertyEditors.Aliases.Tags => "Tags",

            Constants.PropertyEditors.Aliases.CheckBoxList => "List",
            Constants.PropertyEditors.Aliases.DropDownListFlexible => "List",
            Constants.PropertyEditors.Aliases.RadioButtonList => "List",

            Constants.PropertyEditors.Aliases.ColorPickerEyeDropper => "Picker",
            Constants.PropertyEditors.Aliases.ColorPicker => "Picker",
            Constants.PropertyEditors.Aliases.ContentPicker => "Picker",
            Constants.PropertyEditors.Aliases.MediaPicker => "Picker",
            Constants.PropertyEditors.Aliases.MultipleMediaPicker => "Picker",
            Constants.PropertyEditors.Aliases.MemberPicker => "Picker",
            Constants.PropertyEditors.Aliases.MemberGroupPicker => "Picker",
            Constants.PropertyEditors.Aliases.MultiNodeTreePicker => "Picker",
            Constants.PropertyEditors.Aliases.MediaPicker3 => "Picker",
            Constants.PropertyEditors.Aliases.UserPicker => "Picker",
            Constants.PropertyEditors.Aliases.MultiUrlPicker => "Picker",
            Constants.PropertyEditors.Aliases.PickerRelations => "Picker",

            Constants.PropertyEditors.Aliases.Decimal => "Text",
            Constants.PropertyEditors.Aliases.Slider => "Text",
            Constants.PropertyEditors.Aliases.Integer => "Text",
            Constants.PropertyEditors.Aliases.DateTime => "Text",
            Constants.PropertyEditors.Aliases.MultipleTextstring => "Text",
            Constants.PropertyEditors.Aliases.TextBox => "Text",
            Constants.PropertyEditors.Aliases.TextArea => "Text",
            Constants.PropertyEditors.Aliases.TinyMce => "Text",
            Constants.PropertyEditors.Aliases.EmailAddress => "Text",
            Constants.PropertyEditors.Aliases.MarkdownEditor => "Text",
            Constants.PropertyEditors.Aliases.Label => "Text",

            _ => ResolveDataTypeFolderName(dataType)
        };
        return folder;
    }
}
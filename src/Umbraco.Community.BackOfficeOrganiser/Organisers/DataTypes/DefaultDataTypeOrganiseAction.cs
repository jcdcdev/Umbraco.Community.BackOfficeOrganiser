using Umbraco.Community.BackOfficeOrganiser.Extensions;
using Umbraco.Community.BackOfficeOrganiser.Models;
using Microsoft.Extensions.Options;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;

namespace Umbraco.Community.BackOfficeOrganiser.Organisers.DataTypes;

public class DefaultDataTypeOrganiseAction : IDataTypeOrganiseAction
{
    private readonly BackOfficeOrganiserOptions _options;

    public DefaultDataTypeOrganiseAction(IOptions<BackOfficeOrganiserOptions> options)
    {
        _options = options.Value;
    }

    public bool CanMove(IDataType dataType, IDataTypeService dataTypeService) => true;

    public void Move(IDataType dataType, IDataTypeService dataTypeService)
    {
        var parentFolderId = -1;
        if (dataType.IsInternalUmbracoEditor())
        {
            var internalFolder = dataTypeService.GetOrCreateFolder(_options.DataTypes.InternalFolderName);
            parentFolderId = internalFolder.Id;
        }
        else if (dataType.IsUmbracoEditor())
        {
            var internalFolder = dataTypeService.GetOrCreateFolder(_options.DataTypes.CustomFolderName);
            parentFolderId = internalFolder.Id;
        }
        else
        {
            var internalFolder = dataTypeService.GetOrCreateFolder(_options.DataTypes.ThirdPartyFolderName);
            parentFolderId = internalFolder.Id;
        }

        var folder = GetFolderName(dataType);

        var dataTypeFolder = dataTypeService.GetOrCreateFolder(folder, parentFolderId);
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
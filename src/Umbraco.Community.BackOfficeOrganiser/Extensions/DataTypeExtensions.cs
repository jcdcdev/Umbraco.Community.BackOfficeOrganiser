using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Models;
using Umbraco.Extensions;

namespace Umbraco.Community.BackOfficeOrganiser.Extensions;

public static class DataTypeExtensions
{
    private static readonly Guid[] Guids =
    {
        Constants.DataTypes.Guids.ContentPickerGuid,
        Constants.DataTypes.Guids.MemberPickerGuid,
        Constants.DataTypes.Guids.MediaPickerGuid,
        Constants.DataTypes.Guids.MultipleMediaPickerGuid,
        Constants.DataTypes.Guids.MediaPicker3Guid,
        Constants.DataTypes.Guids.MediaPicker3MultipleGuid,
        Constants.DataTypes.Guids.MediaPicker3SingleImageGuid,
        Constants.DataTypes.Guids.MediaPicker3MultipleImagesGuid,
        Constants.DataTypes.Guids.RelatedLinksGuid,
        Constants.DataTypes.Guids.MemberGuid,
        Constants.DataTypes.Guids.ImageCropperGuid,
        Constants.DataTypes.Guids.TagsGuid,
        Constants.DataTypes.Guids.ListViewContentGuid,
        Constants.DataTypes.Guids.ListViewMediaGuid,
        Constants.DataTypes.Guids.ListViewMembersGuid,
        Constants.DataTypes.Guids.DatePickerWithTimeGuid,
        Constants.DataTypes.Guids.ApprovedColorGuid,
        Constants.DataTypes.Guids.DropdownMultipleGuid,
        Constants.DataTypes.Guids.RadioboxGuid,
        Constants.DataTypes.Guids.DatePickerGuid,
        Constants.DataTypes.Guids.DropdownGuid,
        Constants.DataTypes.Guids.CheckboxListGuid,
        Constants.DataTypes.Guids.CheckboxGuid,
        Constants.DataTypes.Guids.NumericGuid,
        Constants.DataTypes.Guids.RichtextEditorGuid,
        Constants.DataTypes.Guids.TextstringGuid,
        Constants.DataTypes.Guids.TextareaGuid,
        Constants.DataTypes.Guids.UploadGuid,
        Constants.DataTypes.Guids.UploadVideoGuid,
        Constants.DataTypes.Guids.UploadAudioGuid,
        Constants.DataTypes.Guids.UploadArticleGuid,
        Constants.DataTypes.Guids.UploadVectorGraphicsGuid,
        Constants.DataTypes.Guids.LabelStringGuid,
        Constants.DataTypes.Guids.LabelIntGuid,
        Constants.DataTypes.Guids.LabelBigIntGuid,
        Constants.DataTypes.Guids.LabelDateTimeGuid,
        Constants.DataTypes.Guids.LabelTimeGuid,
        Constants.DataTypes.Guids.LabelDecimalGuid,
    };

    private static readonly string[] Aliases =
    {
        Constants.PropertyEditors.Aliases.BlockList,
        Constants.PropertyEditors.Aliases.CheckBoxList,
        Constants.PropertyEditors.Aliases.ColorPicker,
        Constants.PropertyEditors.Aliases.ColorPickerEyeDropper,
        Constants.PropertyEditors.Aliases.ContentPicker,
        Constants.PropertyEditors.Aliases.DateTime,
        Constants.PropertyEditors.Aliases.DropDownListFlexible,
        Constants.PropertyEditors.Aliases.Grid,
        Constants.PropertyEditors.Aliases.ImageCropper,
        Constants.PropertyEditors.Aliases.Integer,
        Constants.PropertyEditors.Aliases.Decimal,
        Constants.PropertyEditors.Aliases.ListView,
        Constants.PropertyEditors.Aliases.MediaPicker,
        Constants.PropertyEditors.Aliases.MediaPicker3,
        Constants.PropertyEditors.Aliases.MultipleMediaPicker,
        Constants.PropertyEditors.Aliases.MemberPicker,
        Constants.PropertyEditors.Aliases.MemberGroupPicker,
        Constants.PropertyEditors.Aliases.MultiNodeTreePicker,
        Constants.PropertyEditors.Aliases.MultipleTextstring,
        Constants.PropertyEditors.Aliases.Label,
        Constants.PropertyEditors.Aliases.PickerRelations,
        Constants.PropertyEditors.Aliases.RadioButtonList,
        Constants.PropertyEditors.Aliases.Slider,
        Constants.PropertyEditors.Aliases.Tags,
        Constants.PropertyEditors.Aliases.TextBox,
        Constants.PropertyEditors.Aliases.TextArea,
        Constants.PropertyEditors.Aliases.TinyMce,
        Constants.PropertyEditors.Aliases.Boolean,
        Constants.PropertyEditors.Aliases.MarkdownEditor,
        Constants.PropertyEditors.Aliases.UserPicker,
        Constants.PropertyEditors.Aliases.UploadField,
        Constants.PropertyEditors.Aliases.EmailAddress,
        Constants.PropertyEditors.Aliases.NestedContent,
        Constants.PropertyEditors.Aliases.MultiUrlPicker,
    };

    public static bool IsUmbracoEditor(this IDataType dataType) =>
        Aliases.InvariantContains(dataType.EditorAlias);

    public static bool IsInternalUmbracoEditor(this IDataType dataType) =>
        Guids.Contains(dataType.Key);
}
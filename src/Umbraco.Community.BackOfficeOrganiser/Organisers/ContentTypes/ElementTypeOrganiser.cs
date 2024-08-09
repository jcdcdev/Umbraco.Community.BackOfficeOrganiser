using jcdcdev.Umbraco.Core.Extensions;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.PropertyEditors;
using Umbraco.Cms.Core.Services;
using Umbraco.Extensions;

namespace Umbraco.Community.BackOfficeOrganiser.Organisers.ContentTypes;

public class ElementTypeOrganiser(IDataTypeService dataTypeService) : IContentTypeOrganiseAction
{
    public bool CanMove(IContentType contentType, IContentTypeService contentTypeService) => contentType.IsElement;

    public void Move(IContentType contentType, IContentTypeService contentTypeService)
    {
        var nestedContentDataTypes = dataTypeService.GetByEditorAlias(Umbraco.Cms.Core.Constants.PropertyEditors.Aliases.NestedContent).Select(x => x.ConfigurationAs<NestedContentConfiguration>());
        var blockGridDataTypes = dataTypeService.GetByEditorAlias(Umbraco.Cms.Core.Constants.PropertyEditors.Aliases.BlockGrid).Select(x => x.ConfigurationAs<BlockGridConfiguration>());
        var blockListDataTypes = dataTypeService.GetByEditorAlias(Umbraco.Cms.Core.Constants.PropertyEditors.Aliases.BlockList).Select(x => x.ConfigurationAs<BlockListConfiguration>());

        var nestedContentContentTypeAliases = new List<string>();
        var gridContentTypeKeys = new List<Guid>();
        var blockContentTypeKeys = new List<Guid>();
        foreach (var blockGridDataType in blockGridDataTypes)
        {
            foreach (var blockGrid in blockGridDataType?.Blocks ?? [])
            {
                gridContentTypeKeys.Add(blockGrid.ContentElementTypeKey);
                if (blockGrid.SettingsElementTypeKey.HasValue)
                {
                    gridContentTypeKeys.Add(blockGrid.SettingsElementTypeKey.Value);
                }
            }
        }

        foreach (var blockListDataType in blockListDataTypes)
        {
            foreach (var blockList in blockListDataType?.Blocks ?? [])
            {
                blockContentTypeKeys.Add(blockList.ContentElementTypeKey);
                if (blockList.SettingsElementTypeKey.HasValue)
                {
                    blockContentTypeKeys.Add(blockList.SettingsElementTypeKey.Value);
                }
            }
        }

        foreach (var nestedContentDataType in nestedContentDataTypes)
        {
            if (nestedContentDataType == null)
            {
                continue;
            }

            var aliases = nestedContentDataType.ContentTypes?.Select(x => x.Alias).WhereNotNull() ?? [];
            nestedContentContentTypeAliases.AddRange(aliases);
        }

        var isNestedContent = nestedContentContentTypeAliases.Contains(contentType.Alias);
        var isBlockGrid = gridContentTypeKeys.Contains(contentType.Key);
        var isBlockList = blockContentTypeKeys.Contains(contentType.Key);

        var parent = contentTypeService.GetOrCreateFolder("Element Types");
        var folderName = string.Empty;
        if (isNestedContent && !isBlockGrid && !isBlockList)
        {
            folderName = "Nested Content";
        }
        else if (isBlockGrid && !isNestedContent && !isBlockList)
        {
            folderName = "Block Grid";
        }
        else if (isBlockList && !isNestedContent && !isBlockGrid)
        {
            folderName = "Block List";
        }
        if (!string.IsNullOrWhiteSpace(folderName))
        {
            parent = contentTypeService.GetOrCreateFolder(folderName, parent.Id);
        }

        contentTypeService.Move(contentType, parent.Id);
    }
}

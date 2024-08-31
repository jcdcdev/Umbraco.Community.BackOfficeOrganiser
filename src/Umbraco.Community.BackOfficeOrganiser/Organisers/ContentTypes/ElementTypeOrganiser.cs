using jcdcdev.Umbraco.Core.Extensions;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.PropertyEditors;
using Umbraco.Cms.Core.Services;
using Umbraco.Extensions;

namespace Umbraco.Community.BackOfficeOrganiser.Organisers.ContentTypes;

public class ElementTypeOrganiser : IContentTypeOrganiseAction
{
    private readonly IDataTypeService _dataTypeService;

    public ElementTypeOrganiser(IDataTypeService dataTypeService)
    {
        _dataTypeService = dataTypeService;
    }

    public bool CanMove(IContentType contentType, IContentTypeService contentTypeService) => contentType.IsElement;

    public void Move(IContentType contentType, IContentTypeService contentTypeService)
    {
        var nestedContentDataTypes = _dataTypeService.GetByEditorAlias(Umbraco.Cms.Core.Constants.PropertyEditors.Aliases.NestedContent).Select(x => x.ConfigurationAs<NestedContentConfiguration>());
        var blockGridDataTypes = _dataTypeService.GetByEditorAlias(Umbraco.Cms.Core.Constants.PropertyEditors.Aliases.BlockGrid).Select(x => x.ConfigurationAs<BlockGridConfiguration>());
        var blockListDataTypes = _dataTypeService.GetByEditorAlias(Umbraco.Cms.Core.Constants.PropertyEditors.Aliases.BlockList).Select(x => x.ConfigurationAs<BlockListConfiguration>());

        var nestedContentContentTypeAliases = new List<string>();
        var gridContentTypeKeys = new List<Guid>();
        var blockContentTypeKeys = new List<Guid>();
        foreach (var blockGridDataType in blockGridDataTypes)
        {
            foreach (var blockGrid in blockGridDataType?.Blocks ?? Array.Empty<BlockGridConfiguration.BlockGridBlockConfiguration>())
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
            foreach (var blockList in blockListDataType?.Blocks ?? Array.Empty<BlockListConfiguration.BlockConfiguration>())
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

            var aliases = nestedContentDataType.ContentTypes?.Select(x => x.Alias).WhereNotNull() ?? Array.Empty<string>();
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
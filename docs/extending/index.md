## Extending

You can implement your own `Organise Action`, a method that determines where a type should be moved to. Implement the following interfaces:

- `Document Types` => `IContentTypeOrganiseAction`
- `Media Types` => `IMediaTypeOrganiseAction`
- `Member Types` => `IMemberTypeOrganiseAction`
- `Data Types` => `IDataTypeOrganiseAction`

### Example
```csharp title="ExampleContentTypeOrganiseAction.cs"
using jcdcdev.Umbraco.Core.Extensions;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;

namespace Umbraco.Community.BackOfficeOrganiser.Organisers.ContentTypes;

public class ExampleContentTypeOrganiseAction : IContentTypeOrganiseAction
{
    // Handle all but container types (Folders)
    public bool CanMove(IContentType contentType, IContentTypeService contentTypeService) => !contentType.IsContainer;

    public void Move(IContentType contentType, IContentTypeService contentTypeService)
    {
        var folderId = -1;
        var folderName = string.Empty;
        var isComposition = contentTypeService.GetComposedOf(contentType.Id).Any();

        if (contentType.AllowedTemplates?.Any() ?? false)
        {
            folderName = "Pages";
        }
        else if (isComposition)
        {
            folderName = "Compositions";
        }
        else if (contentType.IsElement)
        {
            folderName = "Element Types";
        }

        if (!folderName.IsNullOrWhiteSpace())
        {
            folderId = contentTypeService.GetOrCreateFolder(folderName).Id;
        }

        contentTypeService.Move(contentType, folderId);
    }
}

public class Composer : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        // Make sure you register your action BEFORE the default!
        builder.ContentTypeOrganiseActions().Insert<ExampleContentTypeOrganiseAction>();
    }
}
```
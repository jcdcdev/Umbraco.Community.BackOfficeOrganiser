# Umbraco.Community.BackOfficeOrganiser

[![Umbraco Version](https://img.shields.io/badge/Umbraco-10.4+-%233544B1?style=flat&logo=umbraco)](https://umbraco.com/products/umbraco-cms/)
[![NuGet](https://img.shields.io/nuget/vpre/Umbraco.Community.BackOfficeOrganiser?color=0273B3)](https://www.nuget.org/packages/Umbraco.Community.BackOfficeOrganiser)
[![GitHub license](https://img.shields.io/github/license/jcdcdev/Umbraco.Community.BackOfficeOrganiser?color=8AB803)](../LICENSE)
[![Downloads](https://img.shields.io/nuget/dt/Umbraco.Community.BackOfficeOrganiser?color=cc9900)](https://www.nuget.org/packages/Umbraco.Community.BackOfficeOrganiser/)

Is your Backoffice a bit untidy?

- Single-click (and opinionated) organiser for
    - Document Types
    - Media Types
    - Member Types
    - Data Types

![A screenshot of the Back Office Organiser in action](https://raw.githubusercontent.com/jcdcdev/Umbraco.Community.BackOfficeOrganiser/main/docs/screenshots/backoffice.png)

## Quick Start

- Go to the backoffice
- Click `Settings`
- Click `Organise`
- Select the types you wish to organise
- Click submit and confirm
- Refresh your page and enjoy a cleaner backoffice ✨

## Configuration
Add the following to your `appsettings.json` file

```JSON
	"BackOfficeOrganiser": {
		"DataTypes": {
			"InternalFolderName": "Internal",
			"ThirdPartyFolderName": "Third Party",
			"CustomFolderName": "Custom"
		}
	}
```

## Extending

You can implement your own `Organise Action`, a method that determines where a type should be moved to. Implement the following interfaces:

- `Document Types` => `IContentTypeOrganiseAction`
- `Media Types` => `IMediaTypeOrganiseAction`
- `Member Types` => `IMemberTypeOrganiseAction`
- `Data Types` => `IDataTypeOrganiseAction`

### Example
```csharp
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

## Contributing

Contributions to this package are most welcome! Please read the [Contributing Guidelines](CONTRIBUTING.md).

## Acknowledgments (thanks!)

- LottePitcher - [opinionated-package-starter](https://github.com/LottePitcher/opinionated-package-starter)
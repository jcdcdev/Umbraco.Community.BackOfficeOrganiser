# Umbraco.Community.BackOfficeOrganiser

[![Umbraco Marketplace](https://img.shields.io/badge/Umbraco-Marketplace-%233544B1?style=flat&logo=umbraco)](https://marketplace.umbraco.com/package/umbraco.community.backofficeorganiser)
[![GitHub License](https://img.shields.io/github/license/jcdcdev/Umbraco.Community.BackOfficeOrganiser?color=8AB803&label=License&logo=github)](https://github.com/jcdcdev/Umbraco.Community.BackOfficeOrganiser/blob/main/LICENSE)
[![NuGet Downloads](https://img.shields.io/nuget/dt/Umbraco.Community.BackOfficeOrganiser?color=cc9900&label=Downloads&logo=nuget)](https://www.nuget.org/packages/Umbraco.Community.BackOfficeOrganiser/)

Is your backoffice a bit untidy?

- Single-click (and opinionated) organiser for
    - Document Types
    - Media Types
    - Member Types
    - Data Types
- Automatically sorts on save (configurable)

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
{
  "BackOfficeOrganiser": {
    "DataTypes": {
      "InternalFolderName": "Internal",
      "ThirdPartyFolderName": "Third Party",
      "CustomFolderName": "Custom",
      "OrganiseOnSave": true
    },
    "ContentTypes": {
      "OrganiseOnSave": true
    },
    "MediaTypes": {
      "OrganiseOnSave": true
    },
    "MemberTypes": {
      "OrganiseOnSave": true
    }
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

Contributions to this package are most welcome! Please read the [Contributing Guidelines](https://github.com/jcdcdev/Umbraco.Community.BackOfficeOrganiser/blob/main/.github/CONTRIBUTING.md).

## Acknowledgments (thanks!)

- LottePitcher - [opinionated-package-starter](https://github.com/LottePitcher/opinionated-package-starter)
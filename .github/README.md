# Umbraco.Community.BackOfficeOrganiser

[![Umbraco Marketplace](https://img.shields.io/badge/Umbraco%20Marketplace-%23f5c1bc?logo=umbraco&logoColor=162335)](https://marketplace.umbraco.com/package/Umbraco.Community.BackOfficeOrganiser)
[![GitHub](https://img.shields.io/badge/GitHub-1?logo=github&color=232925)](https://github.com/jcdcdev/Umbraco.Community.BackOfficeOrganiser)
[![NuGet Downloads](https://img.shields.io/nuget/dt/Umbraco.Community.BackOfficeOrganiser?labelColor=4536d3&color=4536d3&label=NuGet&logo=nuget)](https://www.nuget.org/packages/Umbraco.Community.BackOfficeOrganiser)
[![Project Website](https://img.shields.io/badge/Project%20Website-jcdcdev?color=3c4834&logo=data:image/svg+xml;base64,PHN2ZyB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciIHdpZHRoPSIxNiIgaGVpZ2h0PSIxNiIgZmlsbD0id2hpdGUiIGNsYXNzPSJiaSBiaS1wYy1kaXNwbGF5IiB2aWV3Qm94PSIwIDAgMTYgMTYiPgogIDxwYXRoIGQ9Ik04IDFhMSAxIDAgMCAxIDEtMWg2YTEgMSAwIDAgMSAxIDF2MTRhMSAxIDAgMCAxLTEgMUg5YTEgMSAwIDAgMS0xLTF6bTEgMTMuNWEuNS41IDAgMSAwIDEgMCAuNS41IDAgMCAwLTEgMG0yIDBhLjUuNSAwIDEgMCAxIDAgLjUuNSAwIDAgMC0xIDBNOS41IDFhLjUuNSAwIDAgMCAwIDFoNWEuNS41IDAgMCAwIDAtMXpNOSAzLjVhLjUuNSAwIDAgMCAuNS41aDVhLjUuNSAwIDAgMCAwLTFoLTVhLjUuNSAwIDAgMC0uNS41TTEuNSAyQTEuNSAxLjUgMCAwIDAgMCAzLjV2N0ExLjUgMS41IDAgMCAwIDEuNSAxMkg2djJoLS41YS41LjUgMCAwIDAgMCAxSDd2LTRIMS41YS41LjUgMCAwIDEtLjUtLjV2LTdhLjUuNSAwIDAgMSAuNS0uNUg3VjJ6Ii8+Cjwvc3ZnPg==)](https://jcdc.dev/umbraco-packages/back-office-organiser)


Is your Backoffice a bit untidy?

- Single-click (and opinionated) organiser for
    - Document Types
    - Media Types
    - Member Types
    - Data Types

## Installation

### Install Package

```powershell
dotnet add package Umbraco.Community.BackOfficeOrganiser
```

## Quick Start

- Go to the backoffice
- Click `Settings`
- Click `Organise`
- Select the types you wish to organise
- Click submit and confirm
- Refresh your page and enjoy a cleaner backoffice ✨

## Configuration

Add the following to your `appsettings.json` file

```json title="appsettings.json"
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

## Contributing

Contributions to this package are most welcome! Please visit the [Contributing](https://github.com/jcdcdev/Umbraco.Community.BackOfficeOrganiser/contribute) page.

## Acknowledgements (Thanks)

- LottePitcher  - [opinionated-package-starter](https://github.com/LottePitcher/opinionated-package-starter)




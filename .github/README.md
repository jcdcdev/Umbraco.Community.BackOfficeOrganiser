# Back Office Organiser

[![Documentation](https://jcdc.dev/badge/Documentation/primary/book)](https://docs.jcdc.dev/umbraco-community-backofficeorganiser/latest)
[![Umbraco Marketplace](https://jcdc.dev/badge/Umbraco%20Marketplace/umbraco/umbraco)](https://marketplace.umbraco.com/package/Umbraco.Community.BackOfficeOrganiser)
[![GitHub](https://jcdc.dev/badge/GitHub/github/github)](https://github.com/jcdcdev/Umbraco.Community.BackOfficeOrganiser)
[![NuGet package downloads](https://jcdc.dev/badge/nuget/Umbraco.Community.BackOfficeOrganiser)](https://www.nuget.org/packages/Umbraco.Community.BackOfficeOrganiser)
[![Project Website](https://jcdc.dev/badge/Project%20Website/primary/laptop)](https://jcdc.dev/umbraco-packages/back-office-organiser)


Is your Backoffice a bit untidy?

- Single-click (and opinionated) organiser for
    - Document Types
    - Media Types
    - Member Types
    - Data Types

> [!IMPORTANT]
> Version 13 will only receive security updates and no new features.

> Please review the [security policy](https://github.com/jcdcdev/Umbraco.Community.BackOfficeOrganiser?tab=security-ov-file#supported-versions) for more information.

## Installation

### Install Package

```powershell
dotnet add package Umbraco.Community.BackOfficeOrganiser
```

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

## Security

> [!NOTE]
> This project takes security and support seriously.
> Please visit the [Security](https://github.com/jcdcdev/Umbraco.Community.BackOfficeOrganiser?tab=security-ov-file) page for more information.



## Contributing

Contributions to this package are most welcome! Please visit the [Contributing](https://github.com/jcdcdev/Umbraco.Community.BackOfficeOrganiser/contribute) page.

## Acknowledgements

Thank you to the following projects and individuals for their contributions. High five, you rock! 🤘🦄

- LottePitcher  - [opinionated-package-starter](https://github.com/LottePitcher/opinionated-package-starter)




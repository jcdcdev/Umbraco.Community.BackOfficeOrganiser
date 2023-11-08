# Umbraco.Community.BackOfficeOrganiser

[![Downloads](https://img.shields.io/nuget/dt/Umbraco.Community.BackOfficeOrganiser?color=cc9900)](https://www.nuget.org/packages/Umbraco.Community.BackOfficeOrganiser/)
[![NuGet](https://img.shields.io/nuget/vpre/Umbraco.Community.BackOfficeOrganiser?color=0273B3)](https://www.nuget.org/packages/Umbraco.Community.BackOfficeOrganiser)
[![GitHub license](https://img.shields.io/github/license/jcdcdev/Umbraco.Community.BackOfficeOrganiser?color=8AB803)](../LICENSE)

Is your Backoffice a bit untidy?

- Single-click organiser for
    - Data Types
    - Document Types
    - Media Types
    - Member Types

<img alt="A screenshot of the BackOffice Organiser in action" src="https://raw.githubusercontent.com/jcdcdev/Umbraco.Community.BackOfficeOrganiser/main/docs/screenshots/backoffice.png" />

## Quick Start

- Go to the backoffice
- Click `Back Office Organiser`
- Select the types you wish to organise
- Click submit and confirm
- Refresh your page and enjoy a cleaner backoffice 😀

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

It is possible to add your own "organise actions". Documentation ComingSoon™️

## Contributing

Contributions to this package are most welcome! Please read the [Contributing Guidelines](CONTRIBUTING.md).

## Acknowledgments (thanks!)

- LottePitcher - [opinionated-package-starter](https://github.com/LottePitcher/opinionated-package-starter)
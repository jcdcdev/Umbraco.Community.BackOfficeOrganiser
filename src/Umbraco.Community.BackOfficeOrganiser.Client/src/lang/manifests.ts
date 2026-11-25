import {ManifestLocalization} from "@umbraco-cms/backoffice/localization";

export const ManifestLocalizations: Array<ManifestLocalization> = [
	{
		type: 'localization',
		alias: 'BOO.lang.enus',
		name: 'English (US)',
		weight: 0,
		meta: {
			culture: 'en-us'
		},
		// @ts-ignore
		js: () => import('./en-us')
	},
	{
		type: 'localization',
		alias: 'BOO.lang.en',
		name: 'English',
		weight: 0,
		meta: {
			culture: 'en'
		},
		// @ts-ignore
		js: () => import('./en-us')
	},
	{
		type: 'localization',
		alias: 'BOO.lang.engb',
		name: 'English (UK)',
		weight: 0,
		meta: {
			culture: 'en-gb'
		},
		// @ts-ignore
		js: () => import('./en-us')
	},
]

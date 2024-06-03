import type { ManifestDashboard } from "@umbraco-cms/backoffice/extension-registry";

const dashboards: Array<ManifestDashboard> = [
	{
		type: 'dashboard',
		name: 'timedashboard',
		alias: 'umbraco.community.backofficeorganiser',
		elementName: 'timedashboard-dashboard',
		js: () => import("./organiser.dashboard.ts"),
		weight: -10,
		meta: {
			label: 'Backoffice Organiser',
			pathname: 'backoffice-organiser'
		},
		conditions: [
			{
				alias: 'Umb.Condition.SectionAlias',
				match: 'Umb.Section.Settings'
			}
		]
	}
]

export const manifests = [...dashboards];
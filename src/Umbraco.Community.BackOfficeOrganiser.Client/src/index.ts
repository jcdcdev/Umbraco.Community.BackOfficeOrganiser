import {manifests as dashboardManifests} from './dashboards/manifest.ts';
import {UmbEntryPointOnInit} from "@umbraco-cms/backoffice/extension-api";
import {ManifestLocalizations} from "./lang/manifests.ts";
import {BackofficeOrganiserContext} from "./context/organiser.context.ts";

export const onInit: UmbEntryPointOnInit = (_host, extensionRegistry) => {
	extensionRegistry.registerMany([
		...dashboardManifests,
		...ManifestLocalizations
	]);

	new BackofficeOrganiserContext(_host)
};
import {manifests as dashboardManifests} from './dashboards/manifest.ts';
import {UMB_AUTH_CONTEXT} from "@umbraco-cms/backoffice/auth";
import {client} from './api';
import {UmbEntryPointOnInit} from "@umbraco-cms/backoffice/extension-api";
import {ManifestLocalizations} from "./lang/manifests.ts";
import {BackofficeOrganiserContext} from "./context/organiser.context.ts";

export const onInit: UmbEntryPointOnInit = (_host, extensionRegistry) => {
	extensionRegistry.registerMany([
		...dashboardManifests,
		...ManifestLocalizations
	]);

	_host.consumeContext(UMB_AUTH_CONTEXT, (_auth) => {
		if (!_auth) {
			console.error('No auth context found');
			return;
		}

		const config = _auth.getOpenApiConfiguration();
		client.setConfig({
			auth: config.token,
			baseUrl: config.base,
			credentials: config.credentials,
		});

		client.interceptors.request.use(async (request, _options) => {
			const token = await _auth.getLatestToken();
			request.headers.set('Authorization', `Bearer ${token}`);
			return request;
		});

		new BackofficeOrganiserContext(_host)
	});
};
import { UmbControllerHost } from "@umbraco-cms/backoffice/controller-api";
import { UmbDataSourceResponse } from "@umbraco-cms/backoffice/repository";
import { tryExecute } from "@umbraco-cms/backoffice/resources";
import {
	BackOfficeOrganiser,
	type GetUmbracoBackOfficeOrganiserApiV1InfoResponse,
	OrganiseRequest,
	PostUmbracoBackOfficeOrganiserApiV1OrganiseResponse
} from "../api";

export class BackofficeOrganiserDataSource implements IBackofficeOrganiserDataSource {

	#host: UmbControllerHost;

	constructor(host: UmbControllerHost) {
		this.#host = host;
	}

	async organise(data: OrganiseRequest): Promise<UmbDataSourceResponse<PostUmbracoBackOfficeOrganiserApiV1OrganiseResponse>> {
		const options = {
			body: data,
		};
		return await tryExecute(this.#host, BackOfficeOrganiser.postUmbracoBackOfficeOrganiserApiV1Organise(options))
	}

	async getInfo(): Promise<UmbDataSourceResponse<GetUmbracoBackOfficeOrganiserApiV1InfoResponse>> {
		return await tryExecute(this.#host, BackOfficeOrganiser.getUmbracoBackOfficeOrganiserApiV1Info())
	}
}

export interface IBackofficeOrganiserDataSource {
	organise(data: OrganiseRequest): Promise<UmbDataSourceResponse<PostUmbracoBackOfficeOrganiserApiV1OrganiseResponse>>;

	getInfo(): Promise<UmbDataSourceResponse<GetUmbracoBackOfficeOrganiserApiV1InfoResponse>>;
}


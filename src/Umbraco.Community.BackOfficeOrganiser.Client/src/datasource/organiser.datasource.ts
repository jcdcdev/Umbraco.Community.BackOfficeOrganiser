import {UmbControllerHost} from "@umbraco-cms/backoffice/controller-api";
import {UmbDataSourceResponse} from "@umbraco-cms/backoffice/repository";
import {tryExecute} from "@umbraco-cms/backoffice/resources";
import {
	BackOfficeOrganiser,
	OrganiseInfoResponse,
	OrganiseRequest,
	OrganiseResponse,
} from "../api";

export class BackofficeOrganiserDataSource implements IBackofficeOrganiserDataSource {

	#host: UmbControllerHost;

	constructor(host: UmbControllerHost) {
		this.#host = host;
	}

	async organise(data: OrganiseRequest): Promise<UmbDataSourceResponse<OrganiseResponse>> {
		const options = {
			body: data,
		};
		return await tryExecute(this.#host, BackOfficeOrganiser.postOrganise(options))
	}

	async getInfo(): Promise<UmbDataSourceResponse<OrganiseInfoResponse>> {
		return await tryExecute(this.#host, BackOfficeOrganiser.getInfo())
	}
}

export interface IBackofficeOrganiserDataSource {
	organise(data: OrganiseRequest): Promise<UmbDataSourceResponse<OrganiseResponse>>;

	getInfo(): Promise<UmbDataSourceResponse<OrganiseInfoResponse>>;
}


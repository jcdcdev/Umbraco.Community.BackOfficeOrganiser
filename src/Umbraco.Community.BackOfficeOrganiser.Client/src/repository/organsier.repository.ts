import {UmbControllerHost} from "@umbraco-cms/backoffice/controller-api";
import {UmbDataSourceResponse} from "@umbraco-cms/backoffice/repository";
import {UmbControllerBase} from "@umbraco-cms/backoffice/class-api";
import {BackofficeOrganiserDataSource, IBackofficeOrganiserDataSource} from "../datasource/organiser.datasource.ts";
import {type OrganiseInfoResponse, OrganiseRequest, OrganiseResponse} from "../api";

export class BackofficeOrganiserRepository extends UmbControllerBase {
	#resource: IBackofficeOrganiserDataSource;

	constructor(host: UmbControllerHost) {
		super(host);
		this.#resource = new BackofficeOrganiserDataSource(host);
	}

	organise(data: OrganiseRequest): Promise<UmbDataSourceResponse<OrganiseResponse>> {
		return this.#resource.organise(data);
	}

	getInfo(): Promise<UmbDataSourceResponse<OrganiseInfoResponse>> {
		return this.#resource.getInfo();
	}
}


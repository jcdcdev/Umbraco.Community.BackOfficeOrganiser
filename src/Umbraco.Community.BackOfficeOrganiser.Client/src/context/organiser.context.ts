import {UmbControllerBase} from "@umbraco-cms/backoffice/class-api";
import {UmbControllerHost} from "@umbraco-cms/backoffice/controller-api";
import {UmbDataSourceResponse} from "@umbraco-cms/backoffice/repository";
import {UmbContextToken} from "@umbraco-cms/backoffice/context-api";
import {BackofficeOrganiserRepository} from "../repository/organsier.repository.ts";
import {type OrganiseInfoResponse, OrganiseRequest, OrganiseResponse} from "../api";

export class BackofficeOrganiserContext extends UmbControllerBase {
	#repository: BackofficeOrganiserRepository;

	constructor(host: UmbControllerHost) {
		super(host);
		this.provideContext(BACKOFFICE_ORGANISER_CONTEXT_TOKEN, this);
		this.#repository = new BackofficeOrganiserRepository(this);
	}

	async organise(data: OrganiseRequest): Promise<UmbDataSourceResponse<OrganiseResponse>> {
		return await this.#repository.organise(data);
	}

	async getInfo(): Promise<UmbDataSourceResponse<OrganiseInfoResponse>> {
		return await this.#repository.getInfo();
	}
}

export const BACKOFFICE_ORGANISER_CONTEXT_TOKEN =
	new UmbContextToken<BackofficeOrganiserContext>("BackofficeOrganiserContext");
import {UmbControllerBase} from "@umbraco-cms/backoffice/class-api";
import {UmbControllerHost} from "@umbraco-cms/backoffice/controller-api";
import {UmbDataSourceResponse} from "@umbraco-cms/backoffice/repository";
import {UmbContextToken} from "@umbraco-cms/backoffice/context-api";
import {BackofficeOrganiserRepository} from "../repository/organsier.repository.ts";
import {PostUmbracoBackofficeorganiserApiOrganiseData, PostUmbracoBackofficeorganiserApiOrganiseResponse} from "../api";

export class BackofficeOrganiserContext extends UmbControllerBase {
	#repository: BackofficeOrganiserRepository;

	constructor(host: UmbControllerHost) {
		super(host);
		this.provideContext(BACKOFFICE_ORGANISER_CONTEXT_TOKEN, this);
		this.#repository = new BackofficeOrganiserRepository(this);
	}

	async organise(data: PostUmbracoBackofficeorganiserApiOrganiseData = {}): Promise<UmbDataSourceResponse<PostUmbracoBackofficeorganiserApiOrganiseResponse>> {
		return await this.#repository.organise(data);
	}
}

export const BACKOFFICE_ORGANISER_CONTEXT_TOKEN =
	new UmbContextToken<BackofficeOrganiserContext>("BackofficeOrganiserContext");
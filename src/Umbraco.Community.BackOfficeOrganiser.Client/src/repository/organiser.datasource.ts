import {UmbControllerHost} from "@umbraco-cms/backoffice/controller-api";
import {UmbDataSourceResponse} from "@umbraco-cms/backoffice/repository";
import {tryExecuteAndNotify} from "@umbraco-cms/backoffice/resources";
import {postUmbracoBackofficeorganiserApiOrganise, PostUmbracoBackofficeorganiserApiOrganiseData, PostUmbracoBackofficeorganiserApiOrganiseResponse} from "../api";

export class BackofficeOrganiserDataSource implements IBackofficeOrganiserDataSource {

	#host: UmbControllerHost;

	constructor(host: UmbControllerHost) {
		this.#host = host;
	}

	async organise(data: PostUmbracoBackofficeorganiserApiOrganiseData = {}): Promise<UmbDataSourceResponse<PostUmbracoBackofficeorganiserApiOrganiseResponse>> {
		return await tryExecuteAndNotify(this.#host, postUmbracoBackofficeorganiserApiOrganise(data))
	}
}

export interface IBackofficeOrganiserDataSource {
	organise(data: PostUmbracoBackofficeorganiserApiOrganiseData): Promise<UmbDataSourceResponse<PostUmbracoBackofficeorganiserApiOrganiseResponse>>;
}


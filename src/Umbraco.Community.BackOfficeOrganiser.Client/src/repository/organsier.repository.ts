import {UmbControllerHost} from "@umbraco-cms/backoffice/controller-api";
import {UmbDataSourceResponse} from "@umbraco-cms/backoffice/repository";
import {UmbControllerBase} from "@umbraco-cms/backoffice/class-api";
import {BackofficeOrganiserDataSource, IBackofficeOrganiserDataSource} from "./organiser.datasource.ts";
import {PostUmbracoBackofficeorganiserApiOrganiseData, PostUmbracoBackofficeorganiserApiOrganiseResponse} from "../api";

export class BackofficeOrganiserRepository extends UmbControllerBase {
	#resource: IBackofficeOrganiserDataSource;

	constructor(host: UmbControllerHost) {
		super(host);
		this.#resource = new BackofficeOrganiserDataSource(host);
	}

	organise(data: PostUmbracoBackofficeorganiserApiOrganiseData = {}): Promise<UmbDataSourceResponse<PostUmbracoBackofficeorganiserApiOrganiseResponse>> {
		return this.#resource.organise(data);
	}
}


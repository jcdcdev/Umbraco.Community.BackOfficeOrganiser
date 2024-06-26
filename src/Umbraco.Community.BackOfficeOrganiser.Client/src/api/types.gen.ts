// This file is auto-generated by @hey-api/openapi-ts

export enum EventMessageTypeModel {
    DEFAULT = 'Default',
    INFO = 'Info',
    ERROR = 'Error',
    SUCCESS = 'Success',
    WARNING = 'Warning'
}

export type NotificationHeaderModel = {
    message: string;
    category: string;
    type: EventMessageTypeModel;
};

export type OrganiseRequest = {
    dataTypes: boolean;
    contentTypes: boolean;
    mediaTypes: boolean;
    memberTypes: boolean;
};

export type OrganiseResponse = {
    readonly error: boolean;
    readonly message: string;
};

export type PostUmbracoBackofficeorganiserApiOrganiseData = {
    requestBody?: OrganiseRequest;
};

export type PostUmbracoBackofficeorganiserApiOrganiseResponse = OrganiseResponse;

export type $OpenApiTs = {
    '/umbraco/backofficeorganiser/api/organise': {
        post: {
            req: PostUmbracoBackofficeorganiserApiOrganiseData;
            res: {
                /**
                 * Success
                 */
                200: OrganiseResponse;
            };
        };
    };
};
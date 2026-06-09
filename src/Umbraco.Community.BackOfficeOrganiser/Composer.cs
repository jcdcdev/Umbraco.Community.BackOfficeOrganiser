using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Api.Common.OpenApi;
using Umbraco.Cms.Api.Management.OpenApi;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Infrastructure.Manifest;
using Umbraco.Community.BackOfficeOrganiser.Core.Composing;
using Umbraco.Community.BackOfficeOrganiser.Core.Models;
using Umbraco.Community.BackOfficeOrganiser.Core.Organisers;
using Umbraco.Community.BackOfficeOrganiser.Core.Services;
using Umbraco.Community.BackOfficeOrganiser.Infrastructure;
using Umbraco.Community.BackOfficeOrganiser.Infrastructure.OrganiseActions;
using Umbraco.Community.BackOfficeOrganiser.Web;

namespace Umbraco.Community.BackOfficeOrganiser;

public class Composer : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        builder.Services.AddOptions<BackOfficeOrganiserOptions>()
            .BindConfiguration(BackOfficeOrganiserOptions.SectionName);

        builder.Services.AddSingleton<IPackageManifestReader, PackageManifestReader>();
        builder.Services.AddSingleton<IBackOfficeOrganiserService, BackOfficeOrganiserService>();

        builder.Services.AddSingleton<DataTypeOrganiser>();
        builder.Services.AddSingleton<ContentTypeOrganiser>();
        builder.Services.AddSingleton<MemberTypeOrganiser>();
        builder.Services.AddSingleton<MediaTypeOrganiser>();

        builder.DataTypeOrganiseActions().Append<DefaultDataTypeOrganiseAction>();
        builder.ContentTypeOrganiseActions().Append<DefaultContentTypeOrganiseAction>();
        builder.MediaTypeOrganiseActions().Append<DefaultMediaTypeOrganiseAction>();
        builder.MemberTypeOrganiseActions().Append<DefaultMemberTypeOrganiseAction>();

        builder.AddNotificationAsyncHandler<DataTypeSavedNotification, BackofficeOrganiserNotificationHandler>();
        builder.AddNotificationAsyncHandler<MediaTypeSavedNotification, BackofficeOrganiserNotificationHandler>();
        builder.AddNotificationAsyncHandler<MemberTypeSavedNotification, BackofficeOrganiserNotificationHandler>();
        builder.AddNotificationAsyncHandler<ContentTypeSavedNotification, BackofficeOrganiserNotificationHandler>();
        
        builder.AddBackOfficeOpenApiDocument(Constants.Api.ApiName, document => document
            .WithTitle(Constants.Api.Title)
            .WithBackOfficeAuthentication());    }
}
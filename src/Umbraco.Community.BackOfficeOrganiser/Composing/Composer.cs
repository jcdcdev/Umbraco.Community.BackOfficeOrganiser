using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Community.BackOfficeOrganiser.Models;
using Umbraco.Community.BackOfficeOrganiser.Organisers.ContentTypes;
using Umbraco.Community.BackOfficeOrganiser.Organisers.DataTypes;
using Umbraco.Community.BackOfficeOrganiser.Organisers.MediaTypes;
using Umbraco.Community.BackOfficeOrganiser.Organisers.MemberTypes;
using Umbraco.Community.BackOfficeOrganiser.Services;

namespace Umbraco.Community.BackOfficeOrganiser.Composing;

public class Composer : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        builder.Services.AddOptions<BackOfficeOrganiserOptions>()
            .BindConfiguration(BackOfficeOrganiserOptions.SectionName);

        builder.Services.AddSingleton<IBackOfficeOrganiserService, BackOfficeOrganiserService>();

        builder.Services.AddSingleton<DataTypeOrganiser>();
        builder.Services.AddSingleton<ContentTypeOrganiser>();
        builder.Services.AddSingleton<MemberTypeOrganiser>();
        builder.Services.AddSingleton<MediaTypeOrganiser>();

        builder.ManifestFilters().Append<ManifestFilter>();

        builder.DataTypeOrganiseActions().Append<DefaultDataTypeOrganiseAction>();
        builder.ContentTypeOrganiseActions().Append<DefaultContentTypeOrganiseAction>();
        builder.MediaTypeOrganiseActions().Append<DefaultMediaTypeOrganiseAction>();
        builder.MemberTypeOrganiseActions().Append<DefaultMemberTypeOrganiseAction>();

        builder.AddNotificationHandler<DataTypeSavedNotification, BackofficeOrganiserNotificationHandler>();
        builder.AddNotificationHandler<ContentTypeSavedNotification, BackofficeOrganiserNotificationHandler>();
        builder.AddNotificationHandler<MemberTypeSavedNotification, BackofficeOrganiserNotificationHandler>();
        builder.AddNotificationHandler<MediaTypeSavedNotification, BackofficeOrganiserNotificationHandler>();
    }
}
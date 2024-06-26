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

        builder.DataTypeOrganiseActions().Append<DefaultDataTypeOrganiseAction>();
        builder.ContentTypeOrganiseActions().Append<DefaultContentTypeOrganiseAction>();
        builder.MediaTypeOrganiseActions().Append<DefaultMediaTypeOrganiseAction>();
        builder.MemberTypeOrganiseActions().Append<DefaultMemberTypeOrganiseAction>();

        builder.AddNotificationAsyncHandler<DataTypeSavedNotification, BackofficeOrganiserNotificationHandler>();
        builder.AddNotificationAsyncHandler<MediaTypeSavedNotification, BackofficeOrganiserNotificationHandler>();
        builder.AddNotificationAsyncHandler<MemberTypeSavedNotification, BackofficeOrganiserNotificationHandler>();
        builder.AddNotificationAsyncHandler<ContentTypeSavedNotification, BackofficeOrganiserNotificationHandler>();
    }
}
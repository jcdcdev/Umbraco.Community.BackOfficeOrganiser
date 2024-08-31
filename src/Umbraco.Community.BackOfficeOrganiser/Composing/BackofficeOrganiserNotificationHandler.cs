using Microsoft.Extensions.Options;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Community.BackOfficeOrganiser.Models;
using Umbraco.Community.BackOfficeOrganiser.Organisers.ContentTypes;
using Umbraco.Community.BackOfficeOrganiser.Organisers.DataTypes;
using Umbraco.Community.BackOfficeOrganiser.Organisers.MediaTypes;
using Umbraco.Community.BackOfficeOrganiser.Organisers.MemberTypes;

namespace Umbraco.Community.BackOfficeOrganiser.Composing;

public class BackofficeOrganiserNotificationHandler(
    DataTypeOrganiser dataTypeOrganiser,
    ContentTypeOrganiser contentTypeOrganiser,
    MediaTypeOrganiser mediaTypeOrganiser,
    MemberTypeOrganiser memberTypeOrganiser,
    IOptions<BackOfficeOrganiserOptions> options)
    :
        INotificationAsyncHandler<DataTypeSavedNotification>,
        INotificationAsyncHandler<ContentTypeSavedNotification>,
        INotificationAsyncHandler<MemberTypeSavedNotification>,
        INotificationAsyncHandler<MediaTypeSavedNotification>
{
    private readonly BackOfficeOrganiserOptions _options = options.Value;

    public async Task HandleAsync(ContentTypeSavedNotification notification, CancellationToken cancellationToken)
    {
        if (!_options.ContentTypes.OrganiseOnSave)
        {
            return;
        }

        foreach (var item in notification.SavedEntities)
        {
            await contentTypeOrganiser.OrganiseAsync(item);
        }
    }

    public async Task HandleAsync(DataTypeSavedNotification notification, CancellationToken cancellationToken)
    {
        if (!_options.DataTypes.OrganiseOnSave)
        {
            return;
        }

        foreach (var dataType in notification.SavedEntities)
        {
            await dataTypeOrganiser.OrganiseAsync(dataType);
        }
    }

    public async Task HandleAsync(MediaTypeSavedNotification notification, CancellationToken cancellationToken)
    {
        if (!_options.MediaTypes.OrganiseOnSave)
        {
            return;
        }

        foreach (var item in notification.SavedEntities)
        {
            await mediaTypeOrganiser.OrganiseAsync(item);
        }
    }

    public async Task HandleAsync(MemberTypeSavedNotification notification, CancellationToken cancellationToken)
    {
        if (!_options.MemberTypes.OrganiseOnSave)
        {
            return;
        }

        foreach (var item in notification.SavedEntities)
        {
            await memberTypeOrganiser.OrganiseAsync(item);
        }
    }
}
using Microsoft.Extensions.Options;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Community.BackOfficeOrganiser.Models;
using Umbraco.Community.BackOfficeOrganiser.Organisers.ContentTypes;
using Umbraco.Community.BackOfficeOrganiser.Organisers.DataTypes;
using Umbraco.Community.BackOfficeOrganiser.Organisers.MediaTypes;
using Umbraco.Community.BackOfficeOrganiser.Organisers.MemberTypes;

namespace Umbraco.Community.BackOfficeOrganiser.Composing;

public class BackofficeOrganiserNotificationHandler :
    INotificationAsyncHandler<DataTypeSavedNotification>,
    INotificationAsyncHandler<ContentTypeSavedNotification>,
    INotificationAsyncHandler<MemberTypeSavedNotification>,
    INotificationAsyncHandler<MediaTypeSavedNotification>
{
    private readonly ContentTypeOrganiser _contentTypeOrganiser;
    private readonly DataTypeOrganiser _dataTypeOrganiser;
    private readonly MediaTypeOrganiser _mediaTypeOrganiser;
    private readonly MemberTypeOrganiser _memberTypeOrganiser;
    private readonly BackOfficeOrganiserOptions _options;

    public BackofficeOrganiserNotificationHandler(
        DataTypeOrganiser dataTypeOrganiser,
        ContentTypeOrganiser contentTypeOrganiser,
        MediaTypeOrganiser mediaTypeOrganiser,
        MemberTypeOrganiser memberTypeOrganiser,
        IOptions<BackOfficeOrganiserOptions> options)
    {
        _dataTypeOrganiser = dataTypeOrganiser;
        _contentTypeOrganiser = contentTypeOrganiser;
        _mediaTypeOrganiser = mediaTypeOrganiser;
        _memberTypeOrganiser = memberTypeOrganiser;
        _options = options.Value;
    }

    public async Task HandleAsync(ContentTypeSavedNotification notification, CancellationToken cancellationToken)
    {
        if (!_options.ContentTypes.OrganiseOnSave)
        {
            return;
        }

        foreach (var item in notification.SavedEntities)
        {
            await _contentTypeOrganiser.OrganiseTypeAsync(item);
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
            await _dataTypeOrganiser.OrganiseAsync(dataType);
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
            await _mediaTypeOrganiser.OrganiseTypeAsync(item);
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
            await _memberTypeOrganiser.OrganiseTypeAsync(item);
        }
    }
}
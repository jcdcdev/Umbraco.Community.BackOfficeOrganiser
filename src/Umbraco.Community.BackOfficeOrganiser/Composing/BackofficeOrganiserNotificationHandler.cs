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
    INotificationHandler<DataTypeSavedNotification>,
    INotificationHandler<ContentTypeSavedNotification>,
    INotificationHandler<MemberTypeSavedNotification>,
    INotificationHandler<MediaTypeSavedNotification>
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

    public void Handle(ContentTypeSavedNotification notification)
    {
        if (!_options.ContentTypes.OrganiseOnSave)
        {
            return;
        }

        foreach (var item in notification.SavedEntities)
        {
            _contentTypeOrganiser.Organise(item);
        }
    }

    public void Handle(DataTypeSavedNotification notification)
    {
        if (!_options.DataTypes.OrganiseOnSave)
        {
            return;
        }

        foreach (var dataType in notification.SavedEntities)
        {
            _dataTypeOrganiser.Organise(dataType);
        }
    }

    public void Handle(MediaTypeSavedNotification notification)
    {
        if (!_options.MediaTypes.OrganiseOnSave)
        {
            return;
        }

        foreach (var item in notification.SavedEntities)
        {
            _mediaTypeOrganiser.Organise(item);
        }
    }

    public void Handle(MemberTypeSavedNotification notification)
    {
        if (!_options.MemberTypes.OrganiseOnSave)
        {
            return;
        }

        foreach (var item in notification.SavedEntities)
        {
            _memberTypeOrganiser.Organise(item);
        }
    }
}
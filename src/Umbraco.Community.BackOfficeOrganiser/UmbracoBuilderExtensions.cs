using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Community.BackOfficeOrganiser.Organisers.ContentTypes;
using Umbraco.Community.BackOfficeOrganiser.Organisers.DataTypes;
using Umbraco.Community.BackOfficeOrganiser.Organisers.MediaTypes;
using Umbraco.Community.BackOfficeOrganiser.Organisers.MemberTypes;

namespace Umbraco.Community.BackOfficeOrganiser;

public static class UmbracoBuilderExtensions
{
    public static DataTypeOrganiseActionCollectionBuilder DataTypeOrganiseActions(this IUmbracoBuilder builder) => builder.WithCollectionBuilder<DataTypeOrganiseActionCollectionBuilder>();

    public static ContentTypeOrganiseActionCollectionBuilder ContentTypeOrganiseActions(this IUmbracoBuilder builder) => builder.WithCollectionBuilder<ContentTypeOrganiseActionCollectionBuilder>();

    public static MediaTypeOrganiseActionCollectionBuilder MediaTypeOrganiseActions(this IUmbracoBuilder builder) => builder.WithCollectionBuilder<MediaTypeOrganiseActionCollectionBuilder>();

    public static MemberTypeOrganiseActionCollectionBuilder MemberTypeOrganiseActions(this IUmbracoBuilder builder) => builder.WithCollectionBuilder<MemberTypeOrganiseActionCollectionBuilder>();
}
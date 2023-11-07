using jcdcdev.Umbraco.BackOfficeOrganiser.Organisers.ContentTypes;
using jcdcdev.Umbraco.BackOfficeOrganiser.Organisers.DataTypes;
using jcdcdev.Umbraco.BackOfficeOrganiser.Organisers.MediaTypes;
using jcdcdev.Umbraco.BackOfficeOrganiser.Organisers.MemberTypes;
using Umbraco.Cms.Core.DependencyInjection;

namespace jcdcdev.Umbraco.BackOfficeOrganiser.Extensions;

public static class UmbracoBuilderExtensions
{
    public static DataTypeOrganiseActionCollectionBuilder DataTypeOrganiseActions(this IUmbracoBuilder builder)
        => builder.WithCollectionBuilder<DataTypeOrganiseActionCollectionBuilder>();
    
    public static ContentTypeOrganiseActionCollectionBuilder ContentTypeOrganiseActions(this IUmbracoBuilder builder)
        => builder.WithCollectionBuilder<ContentTypeOrganiseActionCollectionBuilder>();
    
    public static MediaTypeOrganiseActionCollectionBuilder MediaTypeOrganiseActions(this IUmbracoBuilder builder)
        => builder.WithCollectionBuilder<MediaTypeOrganiseActionCollectionBuilder>();
    
    public static MemberTypeOrganiseActionCollectionBuilder MemberTypeOrganiseActions(this IUmbracoBuilder builder)
        => builder.WithCollectionBuilder<MemberTypeOrganiseActionCollectionBuilder>();
}
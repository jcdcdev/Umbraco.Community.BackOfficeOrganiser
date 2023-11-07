using jcdcdev.Umbraco.BackOfficeOrganiser.Extensions;
using jcdcdev.Umbraco.BackOfficeOrganiser.Models;
using jcdcdev.Umbraco.BackOfficeOrganiser.Organisers;
using jcdcdev.Umbraco.BackOfficeOrganiser.Organisers.ContentTypes;
using jcdcdev.Umbraco.BackOfficeOrganiser.Organisers.DataTypes;
using jcdcdev.Umbraco.BackOfficeOrganiser.Organisers.MediaTypes;
using jcdcdev.Umbraco.BackOfficeOrganiser.Organisers.MemberTypes;
using Lucene.Net.Search;
using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Core.Models;

namespace jcdcdev.Umbraco.BackOfficeOrganiser;

public class Composer : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        builder.Services.AddOptions<BackOfficeOrganiserOptions>()
            .BindConfiguration(BackOfficeOrganiserOptions.SectionName);

        builder.Services.AddSingleton<IBackOfficeOrganiserService, BackOfficeOrganiserService>();

        builder.Services.AddSingleton<IBackOfficeOrganiser<IDataType>, DataTypeOrganiser>();
        builder.Services.AddSingleton<IBackOfficeOrganiser<IContentType>, ContentTypeOrganiser>();
        builder.Services.AddSingleton<IBackOfficeOrganiser<IMemberType>, MemberTypeOrganiser>();
        builder.Services.AddSingleton<IBackOfficeOrganiser<IMediaType>, MediaTypeOrganiser>();

        builder.ManifestFilters().Append<ManifestFilter>();
        
        builder.DataTypeOrganiseActions().Append<DefaultDataTypeOrganiseAction>();
        builder.ContentTypeOrganiseActions().Append<DefaultContentTypeOrganiseAction>();
        builder.MediaTypeOrganiseActions().Append<DefaultMediaTypeOrganiseAction>();
        builder.MemberTypeOrganiseActions().Append<DefaultMemberTypeOrganiseAction>();
    }
}
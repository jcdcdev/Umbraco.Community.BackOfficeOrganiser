using Umbraco.Community.BackOfficeOrganiser.Extensions;
using Umbraco.Community.BackOfficeOrganiser.Models;
using Umbraco.Community.BackOfficeOrganiser.Organisers;
using Umbraco.Community.BackOfficeOrganiser.Organisers.ContentTypes;
using Umbraco.Community.BackOfficeOrganiser.Organisers.DataTypes;
using Umbraco.Community.BackOfficeOrganiser.Organisers.MediaTypes;
using Umbraco.Community.BackOfficeOrganiser.Organisers.MemberTypes;
using Lucene.Net.Search;
using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Core.Models;

namespace Umbraco.Community.BackOfficeOrganiser;

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
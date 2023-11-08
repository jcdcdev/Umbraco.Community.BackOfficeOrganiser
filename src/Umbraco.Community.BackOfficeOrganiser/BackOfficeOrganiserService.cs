using Umbraco.Community.BackOfficeOrganiser.Models;
using Umbraco.Community.BackOfficeOrganiser.Organisers;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Models;

namespace Umbraco.Community.BackOfficeOrganiser;

public class BackOfficeOrganiserService : IBackOfficeOrganiserService
{
    private readonly IBackOfficeOrganiser<IDataType> _dataTypeOrganiser;
    private readonly IBackOfficeOrganiser<IContentType> _contentTypeOrganiser;
    private readonly IBackOfficeOrganiser<IMediaType> _mediaTypeOrganiser;
    private readonly IBackOfficeOrganiser<IMemberType> _memberTypeOrganiser;

    public BackOfficeOrganiserService(
        IBackOfficeOrganiser<IDataType> dataTypeOrganiser,
        IBackOfficeOrganiser<IContentType> contentTypeOrganiser,
        IBackOfficeOrganiser<IMediaType> mediaTypeOrganiser,
        IBackOfficeOrganiser<IMemberType> memberTypeOrganiser)
    {
        _dataTypeOrganiser = dataTypeOrganiser;
        _contentTypeOrganiser = contentTypeOrganiser;
        _mediaTypeOrganiser = mediaTypeOrganiser;
        _memberTypeOrganiser = memberTypeOrganiser;
    }
    
    public void OrganiseDataTypes()
    {
        _dataTypeOrganiser.Organise();
    }

    public Attempt<OrganiseType> Organise(OrganiseType organise)
    {
        try
        {
            switch (organise)
            {
                case OrganiseType.ContentTypes:
                    OrganiseContentTypes();
                    break;
                case OrganiseType.MediaTypes:
                    OrganiseMediaTypes();
                    break;
                case OrganiseType.MemberTypes:
                    OrganiseMemberTypes();
                    break;
                case OrganiseType.DataTypes:
                    OrganiseDataTypes();
                    break;
                case OrganiseType.All:
                    OrganiseAll();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(organise), organise,
                        "Failed to determine OrganiseType");
            }
        }
        catch (Exception ex)
        {
            return Attempt<OrganiseType>.Fail(ex);
        }

        return Attempt<OrganiseType>.Succeed(organise);
    }

    private void OrganiseAll()
    {
        OrganiseContentTypes();
        OrganiseMediaTypes();
        OrganiseMemberTypes();
        OrganiseDataTypes();
    }

    private void OrganiseMemberTypes()
    {
       _memberTypeOrganiser.Organise();
    }

    private void OrganiseMediaTypes()
    {
        _mediaTypeOrganiser.Organise();
    }

    private void OrganiseContentTypes()
    {
        _contentTypeOrganiser.Organise();
    }
}
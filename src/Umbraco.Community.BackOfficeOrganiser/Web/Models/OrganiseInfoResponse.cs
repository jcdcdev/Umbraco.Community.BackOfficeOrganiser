namespace Umbraco.Community.BackOfficeOrganiser.Web.Models;

public class OrganiseInfoResponse
{
    public IEnumerable<OrganiseInfoModel> DataTypes { get; set; } = [];
    public IEnumerable<OrganiseInfoModel> ContentTypes { get; set; } = [];
    public IEnumerable<OrganiseInfoModel> MediaTypes { get; set; } = [];
    public IEnumerable<OrganiseInfoModel> MemberTypes { get; set; } = [];
}
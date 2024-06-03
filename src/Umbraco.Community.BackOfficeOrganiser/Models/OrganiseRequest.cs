namespace Umbraco.Community.BackOfficeOrganiser.Models;

public class OrganiseRequest
{
    public bool DataTypes { get; set; }
    public bool ContentTypes { get; set; }
    public bool MediaTypes { get; set; }
    public bool MemberTypes { get; set; }

    public OrganiseType[] GetOrganiseTypes()
    {
        var types = new List<OrganiseType>();

        if (DataTypes)
        {
            types.Add(OrganiseType.DataTypes);
        }

        if (ContentTypes)
        {
            types.Add(OrganiseType.ContentTypes);
        }

        if (MediaTypes)
        {
            types.Add(OrganiseType.MediaTypes);
        }

        if (MemberTypes)
        {
            types.Add(OrganiseType.MemberTypes);
        }

        return types.ToArray();
    }
}
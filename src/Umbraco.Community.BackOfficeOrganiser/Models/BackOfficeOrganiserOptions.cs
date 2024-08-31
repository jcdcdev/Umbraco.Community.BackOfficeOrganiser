namespace Umbraco.Community.BackOfficeOrganiser.Models;

public class BackOfficeOrganiserOptions
{
    public DataTypeOptions DataTypes { get; set; } = new();
    public ContentTypeOptions ContentTypes { get; set; } = new();
    public MemberTypeOptions MemberTypes { get; set; } = new();
    public MediaTypeOptions MediaTypes { get; set; } = new();
    public static string SectionName => "BackOfficeOrganiser";
}
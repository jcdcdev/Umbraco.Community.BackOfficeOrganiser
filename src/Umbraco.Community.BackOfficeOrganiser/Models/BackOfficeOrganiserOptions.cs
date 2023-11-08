namespace Umbraco.Community.BackOfficeOrganiser.Models;

public class BackOfficeOrganiserOptions
{
    public DataTypeOptions DataTypes { get; set; } = new();
    public static string SectionName => "BackOfficeOrganiser";
}
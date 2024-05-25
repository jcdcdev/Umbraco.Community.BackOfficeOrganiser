namespace Umbraco.Community.BackOfficeOrganiser.Organisers;

public interface IBackOfficeOrganiser<T>
{
    Task OrganiseTypeAsync();
}
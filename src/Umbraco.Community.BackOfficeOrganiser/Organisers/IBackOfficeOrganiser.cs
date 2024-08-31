namespace Umbraco.Community.BackOfficeOrganiser.Organisers;

public interface IBackOfficeOrganiser<T>
{
    Task OrganiseAllAsync();
    Task OrganiseAsync(T item);
}
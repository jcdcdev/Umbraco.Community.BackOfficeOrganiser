namespace Umbraco.Community.BackOfficeOrganiser.Organisers;

public interface IBackOfficeOrganiser<T>
{
    void OrganiseAll();
    void Organise(T item);
}
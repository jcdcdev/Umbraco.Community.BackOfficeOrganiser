using Microsoft.Extensions.Logging;

namespace Umbraco.Community.BackOfficeOrganiser.Organisers;

public abstract class BackOfficeOrganiserBase<T> : IBackOfficeOrganiser<T>
{
    public readonly ILogger Logger;

    protected BackOfficeOrganiserBase(ILogger logger)
    {
        Logger = logger;
    }

    protected virtual void PostOrganiseAll()
    {
    }

    public void OrganiseAll()
    {
        Logger.LogInformation("BackOfficeOrganiser: Cleanup for {Type} Started", typeof(T).Name);

        try
        {
            var items = GetAll();
            foreach (var item in items)
            {
                Organise(item);
            }
            PostOrganiseAll();
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "BackOfficeOrganiser: Cleanup for {Type} Failed", typeof(T).Name);
            return;
        }

        Logger.LogInformation("BackOfficeOrganiser: Cleanup for {Type} Complete", typeof(T).Name);
    }

    public abstract void Organise(T item);

    protected abstract List<T> GetAll();
}
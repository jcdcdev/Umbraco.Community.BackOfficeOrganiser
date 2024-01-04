using Microsoft.Extensions.Logging;

namespace Umbraco.Community.BackOfficeOrganiser.Organisers;

public abstract class BackOfficeOrganiserBase<T> : IBackOfficeOrganiser<T>
{
    public readonly ILogger Logger;

    protected BackOfficeOrganiserBase(ILogger logger)
    {
        Logger = logger;
    }

    public void OrganiseType()
    {
        Logger.LogInformation("BackOfficeOrganiser: Cleanup for {Type} Started", typeof(T).Name);

        try
        {
            Organise();
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "BackOfficeOrganiser: Cleanup for {Type} Failed", typeof(T).Name);
            return;
        }

        Logger.LogInformation("BackOfficeOrganiser: Cleanup for {Type} Complete", typeof(T).Name);
    }

    public abstract void Organise();
}
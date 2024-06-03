using Microsoft.Extensions.Logging;
using Umbraco.Community.BackOfficeOrganiser.Organisers.DataTypes;

namespace Umbraco.Community.BackOfficeOrganiser.Organisers;

public abstract class BackOfficeOrganiserBase<T> : IBackOfficeOrganiser<T>
{
    protected readonly ILogger Logger;

    protected BackOfficeOrganiserBase(ILogger logger)
    {
        Logger = logger;
    }

    public async Task OrganiseTypeAsync()
    {
        Logger.LogInformation("BackOfficeOrganiser: Cleanup for {Type} Started", typeof(T).Name);

        try
        {
            await OrganiseAsync();
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "BackOfficeOrganiser: Cleanup for {Type} Failed", typeof(T).Name);
            return;
        }

        Logger.LogInformation("BackOfficeOrganiser: Cleanup for {Type} Complete", typeof(T).Name);
    }

    protected abstract Task OrganiseAsync();
}
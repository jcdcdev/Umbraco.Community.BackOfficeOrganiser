using Umbraco.Community.SimpleDashboards.Core;
using Constants = Umbraco.Cms.Core.Constants;

namespace Umbraco.Community.BackOfficeOrganiser.Dashboards;

public class BackOfficeOrganiserDashboard : SimpleDashboard
{
    public BackOfficeOrganiserDashboard()
    {
        AddSection(Constants.Applications.Settings);
        SetName("Organise");
    }
}
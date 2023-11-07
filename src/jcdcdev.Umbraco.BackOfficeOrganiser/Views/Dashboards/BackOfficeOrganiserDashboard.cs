using Umbraco.Community.SimpleDashboards.Core;
using Constants = Umbraco.Cms.Core.Constants;

namespace jcdcdev.Umbraco.BackOfficeOrganiser.Views.Dashboards;

public class BackOfficeOrganiserDashboard : SimpleDashboard
{
    public BackOfficeOrganiserDashboard()
    {
        AddSection(Constants.Applications.Settings);
        SetName("Back Office Organiser");
    }
}
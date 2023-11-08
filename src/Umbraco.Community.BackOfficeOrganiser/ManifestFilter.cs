using Umbraco.Cms.Core.Manifest;

namespace Umbraco.Community.BackOfficeOrganiser;

internal class ManifestFilter : IManifestFilter
{
    public void Filter(List<PackageManifest> manifests)
    {
        manifests.Add(new PackageManifest
        {
            PackageName = "Umbraco.Community.BackOfficeOrganiser",
            Version = GetType().Assembly.GetName().Version?.ToString(3) ?? "0.1.0",
            AllowPackageTelemetry = true
        });
    }
}
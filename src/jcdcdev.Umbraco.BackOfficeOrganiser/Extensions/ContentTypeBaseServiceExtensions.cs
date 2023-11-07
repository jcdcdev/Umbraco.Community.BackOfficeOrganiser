using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;
using Umbraco.Extensions;

namespace jcdcdev.Umbraco.BackOfficeOrganiser.Extensions;

public static class ContentTypeBaseServiceExtensions
{
    public static IEnumerable<EntityContainer> GetAllContainers<T>(this IContentTypeBaseService<T> service) where T : IContentTypeComposition =>
        service.GetContainers(Array.Empty<int>());

    public static void DeleteAllEmptyContainers<T>(this IContentTypeBaseService<T> service) where T : IContentTypeComposition
    {
        var contentTypes = service.GetAll();
        var lookup = contentTypes.GroupBy(x => x.ParentId).ToLookup(x => x.Key, x => x.Count());
        var containers = service.GetAllContainers();
        foreach (var container in containers)
        {
            var hasChildren = lookup.Contains(container.Id);
            if (hasChildren)
            {
                continue;
            }

            service.DeleteContainer(container.Id);
        }
    }

    public static EntityContainer GetOrCreateFolder<T>(this IContentTypeBaseService<T> service, string folder, int parentId = -1) where T : IContentTypeComposition
    {
        var container = service
            .GetAllContainers()
            .FirstOrDefault(x => x.ParentId == parentId && x.Name.InvariantEquals(folder));

        if (container == null)
        {
            container = service.CreateContainer(parentId, new Guid(), folder).Result?.Entity;
        }

        return container ?? throw new Exception();
    }
}
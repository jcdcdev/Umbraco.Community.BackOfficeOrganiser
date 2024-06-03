using Umbraco.Cms.Core;
using Umbraco.Community.BackOfficeOrganiser.Models;

namespace Umbraco.Community.BackOfficeOrganiser.Services;

public interface IBackOfficeOrganiserService
{
    Task<Attempt<OrganiseType>> OrganiseDataTypesAsync();
    Task<Attempt<OrganiseType>> OrganiseMemberTypesAsync();
    Task<Attempt<OrganiseType>> OrganiseMediaTypesAsync();
    Task<Attempt<OrganiseType>> OrganiseContentTypesAsync();
    Task<Attempt<OrganiseType>> OrganiseAsync(OrganiseType type);
}
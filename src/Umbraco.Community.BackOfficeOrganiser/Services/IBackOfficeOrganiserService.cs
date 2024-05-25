using Umbraco.Cms.Core;
using Umbraco.Community.BackOfficeOrganiser.Models;

namespace Umbraco.Community.BackOfficeOrganiser;

public interface IBackOfficeOrganiserService
{
    Task<Attempt<OrganiseType>> OrganiseAsync(OrganiseType organise);
}
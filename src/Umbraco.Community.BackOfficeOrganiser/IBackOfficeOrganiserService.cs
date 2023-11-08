using Umbraco.Community.BackOfficeOrganiser.Models;
using Umbraco.Cms.Core;

namespace Umbraco.Community.BackOfficeOrganiser;

public interface IBackOfficeOrganiserService
{
    Attempt<OrganiseType> Organise(OrganiseType organise);
}
using jcdcdev.Umbraco.BackOfficeOrganiser.Models;
using Umbraco.Cms.Core;

namespace jcdcdev.Umbraco.BackOfficeOrganiser;

public interface IBackOfficeOrganiserService
{
    Attempt<OrganiseType> Organise(OrganiseType organise);
}
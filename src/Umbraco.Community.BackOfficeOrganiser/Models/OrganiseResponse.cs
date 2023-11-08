namespace Umbraco.Community.BackOfficeOrganiser.Models;

public class OrganiseResponse
{
    private OrganiseResponse(string? message, bool success)
    {
        Message = message ?? string.Empty;
        Error = !success;
    }

    public bool Error { get; }

    public string Message { get; }

    public static OrganiseResponse Success(string? message = null) => new(message, true);

    public static OrganiseResponse Fail(string? message = null) => new(message, false);
}
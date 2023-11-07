namespace jcdcdev.Umbraco.BackOfficeOrganiser.Models;

public class OrganiseResponse
{
    public static OrganiseResponse Success(string? message = null)
    {
        return new OrganiseResponse(message, true);
    }

    public static OrganiseResponse Fail(string? message = null)
    {
        return new OrganiseResponse(message, false);
    }

    private OrganiseResponse(string? message, bool success)
    {
        Message = message ?? string.Empty;
        Error = !success;
    }

    public bool Error { get; }

    public string Message { get; }
}
namespace Services.DataTransferObjects.Request;

public record DtoForAuth
{
    public string? Username { get; init; }
    public string? Password { get; init; }
}
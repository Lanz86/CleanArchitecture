namespace CleanArchitecture.WebUI.Endpoints.OidcConfiguration;

public record OidcConfigurationRequest
{
    public string ClientId { get; set; } = string.Empty;
}

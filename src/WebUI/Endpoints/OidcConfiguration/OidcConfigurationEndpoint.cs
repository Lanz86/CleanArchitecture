using CleanArchitecture.WebUI.Endpoints.Base;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebUI.Endpoints.OidcConfiguration;

public class OidcConfigurationEndpoint : AbstractEndpoint<OidcConfigurationRequest, IDictionary<string,string>>
{
    private readonly IClientRequestParametersProvider _clientRequestParametersProvider;
    private readonly ILogger<OidcConfigurationEndpoint> _logger;

    public OidcConfigurationEndpoint(IClientRequestParametersProvider clientRequestParametersProvider,
                                    ILogger<OidcConfigurationEndpoint> logger)
    {
        _clientRequestParametersProvider = clientRequestParametersProvider;
        _logger = logger;
    }

    public override void Configure()
    {
        Get("/_configuration/{clientId}");
        RoutePrefixOverride(string.Empty);
        AllowAnonymous();
    }

    public override async Task HandleAsync(OidcConfigurationRequest req, CancellationToken ct)
    {
        await SendAsync(_clientRequestParametersProvider.GetClientParameters(HttpContext, req.ClientId));
    }

}

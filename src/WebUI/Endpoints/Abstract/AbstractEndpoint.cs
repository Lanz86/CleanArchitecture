using FastEndpoints;
using MediatR;

namespace CleanArchitecture.WebUI.Endpoints.Base;

public abstract class AbstractEndpoint<TRequest, TResponse> : Endpoint<TRequest, TResponse> where TRequest : notnull, new()
{
    private ISender? _mediator;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}

public abstract class AbstractEndpoint<TRequest> : AbstractEndpoint<TRequest, EmptyResponse> where TRequest : notnull, new()
{

}

public abstract class AbstractEndpoint : AbstractEndpoint<EmptyRequest, EmptyResponse>
{
}

public abstract class AbstractEndpointWithoutRequest<TResponse> : AbstractEndpoint<EmptyRequest, TResponse> { }

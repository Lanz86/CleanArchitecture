using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.WebUI.Endpoints.Base;
using CleanArchitecture.WebUI.Endpoints.Commons.Requestes;

namespace CleanArchitecture.WebUI.Endpoints.TodoLists.DeleteTodoList;

public class DeleteTodoListEndpoint : AbstractEndpoint<BaseRequest>
{
    public override void Configure()
    {
        Delete("/{id}");
        Description(b => b
            .Produces(StatusCodes.Status204NoContent)
            .Produces<NotFoundException>(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError));
        Group<TodoListsGroup>();
    }

    public override async Task HandleAsync(BaseRequest req, CancellationToken ct)
    {
        await Mediator.Send(req, ct);
        await SendNoContentAsync();
    }

}

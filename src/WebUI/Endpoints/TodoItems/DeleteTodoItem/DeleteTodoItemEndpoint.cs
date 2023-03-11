using CleanArchitecture.WebUI.Endpoints.Base;
using CleanArchitecture.WebUI.Endpoints.Commons.Requestes;

namespace CleanArchitecture.WebUI.Endpoints.TodoItems.DeleteTodoItem;

public class DeleteTodoItemEndpoint : AbstractEndpoint<BaseRequest>
{
    public override void Configure()
    {
        Delete("/{id}");
        Group<TodoItemsGroup>();
    }

    public override async Task HandleAsync(BaseRequest req, CancellationToken ct)
    {
        await Mediator.Send(req, ct);
        await SendNoContentAsync();
    }
}

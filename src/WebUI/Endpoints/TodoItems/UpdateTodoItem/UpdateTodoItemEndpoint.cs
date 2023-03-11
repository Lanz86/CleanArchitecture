using CleanArchitecture.Application.TodoItems.Commands.UpdateTodoItem;
using CleanArchitecture.WebUI.Endpoints.Base;

namespace CleanArchitecture.WebUI.Endpoints.TodoItems.UpdateTodoItem;

public class UpdateTodoItemEndpoint : AbstractEndpoint<UpdateTodoItemCommand>
{
    public override void Configure()
    {
        Put("/{id}");
        Group<TodoItemsGroup>();
    }

    public override async Task HandleAsync(UpdateTodoItemCommand req, CancellationToken ct)
    {
        await Mediator.Send(req, ct);
        await SendNoContentAsync();
    }
}

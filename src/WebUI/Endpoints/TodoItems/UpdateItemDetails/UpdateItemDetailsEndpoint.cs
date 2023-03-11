using CleanArchitecture.Application.TodoItems.Commands.UpdateTodoItemDetail;
using CleanArchitecture.WebUI.Endpoints.Base;

namespace CleanArchitecture.WebUI.Endpoints.TodoItems.UpdateItemDetails;

public class UpdateItemDetailsEndpoint : AbstractEndpoint<UpdateTodoItemDetailCommand>
{
    public override void Configure()
    {
        Put("/UpdateItemDetails/{id}");
        Group<TodoItemsGroup>();
    }

    public override async Task HandleAsync(UpdateTodoItemDetailCommand req, CancellationToken ct)
    {
        await Mediator.Send(req, ct);
        await SendNoContentAsync();
    }

}

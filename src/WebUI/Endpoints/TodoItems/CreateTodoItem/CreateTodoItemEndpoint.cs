using CleanArchitecture.Application.TodoItems.Commands.CreateTodoItem;
using CleanArchitecture.WebUI.Endpoints.Base;
using CleanArchitecture.WebUI.Endpoints.Commons.Responses;

namespace CleanArchitecture.WebUI.Endpoints.TodoItems.CreateTodoItem;

public class CreateTodoItemEndpoint : AbstractEndpoint<CreateTodoItemCommand, BaseResponse>
{
    public override void Configure()
    {
        Post("/");
        Group<TodoItemsGroup>();
    }

    public override async Task HandleAsync(CreateTodoItemCommand req, CancellationToken ct)
    {
        var result = await Mediator.Send(req, ct);
        await SendAsync(new BaseResponse { Id = result });
    }
}

using CleanArchitecture.Application.TodoLists.Commands.CreateTodoList;
using CleanArchitecture.WebUI.Endpoints.Base;
using CleanArchitecture.WebUI.Endpoints.Commons.Responses;

namespace CleanArchitecture.WebUI.Endpoints.TodoLists.CreateTodoList;

public class CreateTodoListEndpoint : AbstractEndpoint<CreateTodoListCommand, BaseResponse>
{
    public override void Configure()
    {
        Post("/");
        Group<TodoListsGroup>();
    }

    public override async Task HandleAsync(CreateTodoListCommand req, CancellationToken ct)
    {
        var result = await Mediator.Send(req, ct);
        await SendAsync(new BaseResponse { Id = result });
    }
}

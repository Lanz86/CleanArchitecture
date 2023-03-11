using CleanArchitecture.Application.TodoLists.Queries.GetTodos;
using CleanArchitecture.WebUI.Endpoints.Base;
using CleanArchitecture.WebUI.Endpoints.TodoLists;
using FastEndpoints;

namespace CleanArchitecture.Endpoints.TodoLists.GetTodoLists;


public class GetTodoListsEndpoint : AbstractEndpoint<GetTodosQuery, TodosVm>
{
    public override void Configure()
    {
        Get("/");
        Group<TodoListsGroup>();
    }

    public async override Task HandleAsync(GetTodosQuery req, CancellationToken ct)
    {
        await SendAsync(await Mediator.Send(req));
    }
}

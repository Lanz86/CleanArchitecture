using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.TodoItems.Queries.GetTodoItemsWithPagination;
using CleanArchitecture.WebUI.Endpoints.Base;

namespace CleanArchitecture.WebUI.Endpoints.TodoItems.GetTodoItemsWithPagination;

public class GetTodoItemsWithPaginationEndpoint : AbstractEndpoint<GetTodoItemsWithPaginationQuery, PaginatedList<TodoItemBriefDto>>
{
    public override void Configure()
    {
        Get("/");
        Group<TodoItemsGroup>();
    }

    public override async Task HandleAsync(GetTodoItemsWithPaginationQuery req, CancellationToken ct)
    {
        await SendAsync(await Mediator.Send(req, ct));
    }
}

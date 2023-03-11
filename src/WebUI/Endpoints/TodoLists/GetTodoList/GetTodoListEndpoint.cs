using CleanArchitecture.Application.TodoLists.Queries.ExportTodos;
using CleanArchitecture.WebUI.Endpoints.Base;
using CleanArchitecture.WebUI.Endpoints.Commons.Requestes;

namespace CleanArchitecture.WebUI.Endpoints.TodoLists.GetTodoList;

public class GetTodoListEndpoint : AbstractEndpoint<BaseRequest>
{
    public override void Configure()
    {
        Get("/{id}");
        Group<TodoListsGroup>();
    }

    public override async Task HandleAsync(BaseRequest req, CancellationToken ct)
    {
        var vm = await Mediator.Send(new ExportTodosQuery { ListId = req.Id });
        await SendBytesAsync(vm.Content, vm.ContentType, vm.FileName);
    }
}

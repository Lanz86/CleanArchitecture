using System.ComponentModel.DataAnnotations;
using CleanArchitecture.Application.TodoLists.Commands.UpdateTodoList;
using CleanArchitecture.WebUI.Endpoints.Base;
using FastEndpoints;

namespace CleanArchitecture.WebUI.Endpoints.TodoLists.UpdateTodoList;

public class UpdateTodoListEndpoint : AbstractEndpoint<UpdateTodoListCommand>
{
    public override void Configure()
    {
        Put("/{id}");
        Description(b => b
          .Produces(StatusCodes.Status204NoContent)
          .Produces<ValidationException>(StatusCodes.Status400BadRequest, "application/json+problem")
          .Produces(StatusCodes.Status500InternalServerError));
        Group<TodoListsGroup>();
    }

    public override async Task HandleAsync(UpdateTodoListCommand req, CancellationToken ct)
    {
        await Mediator.Send(req, ct);
        await SendNoContentAsync(ct);
    }

}

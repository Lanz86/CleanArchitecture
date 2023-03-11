using FastEndpoints;

namespace CleanArchitecture.WebUI.Endpoints.TodoLists;

public class TodoListsGroup : Group
{
    public TodoListsGroup()
    {
        Configure("/TodoLists", ep =>
        {
            ep.Description(d => d.WithGroupName("TodoLists"));
        });
    }
}

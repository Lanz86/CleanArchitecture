using FastEndpoints;

namespace CleanArchitecture.WebUI.Endpoints.TodoItems;

public class TodoItemsGroup : Group
{
    public TodoItemsGroup()
    {
        Configure("/TodoItems", ep => 
        {
            ep.Description(x => x.WithGroupName("TodoItems"));
        });
    }
}

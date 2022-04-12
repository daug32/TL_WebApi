namespace TodoList.Domain;

public class Todo
{
    public int Id { get; set; }
    public string Title { get; set; }
    public bool IsDone { get; set; }

    // Used to test 
    public override bool Equals( object? obj )
    {
        if ( obj is not Todo todo ) return false;
        return
            Id == todo.Id &&
            Title == todo.Title &&
            IsDone == todo.IsDone;
    }
}

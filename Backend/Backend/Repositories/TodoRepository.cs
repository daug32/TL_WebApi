using TodoList.Domain;

namespace TodoList.Repositories;

public class TodoRepository : ITodoRepository
{
    private string _table = "Todo";

    public IEnumerable<Todo> GetTodos()
    {
        return BaseRepository.GetAll( _table )
            .Select( el => ParseRow( el ) );
    }

    public Todo Get( int id )
    {
        var row = BaseRepository.GetFirstBy( _table, "Id", id );
        if ( row.Count < 1 ) return new Todo();
        return ParseRow( row );
    }

    public int Create( Todo todo )
    {
        return BaseRepository.Insert( _table, ToRow( todo ) );
    }

    public void Delete( int todoId )
    {
        BaseRepository.DeleteBy( _table, "Id", todoId );
    }

    public void Update( Todo todo )
    {
        BaseRepository.Update( _table, "Id", todo.Id, ToRow( todo ) );
    }

    protected static Todo ParseRow( Dictionary<string, object> row )
    {
        var id = Convert.ToInt32( row[ "Id" ] );
        var title = Convert.ToString( row[ "Title" ] );
        var isDone = Convert.ToBoolean( row[ "IsDone" ] );

        return new Todo() { Id = id, Title = title, IsDone = isDone };
    }

    protected static Dictionary<string, object> ToRow( Todo todo )
    {
        return new Dictionary<string, object>()
        {
            { "Title", todo.Title },
            { "IsDone", todo.IsDone }
        };
    }
}

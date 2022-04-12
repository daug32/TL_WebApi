using TodoList.Domain;

namespace TodoList.Repositories;

public interface ITodoRepository
{
    /// <summary>
    /// Get all todos from database
    /// </summary>
    /// <returns>IEnumerable object containing Todos</returns>
    IEnumerable<Todo> GetTodos();

    /// <summary>
    /// Get Todo from database by id
    /// </summary>
    /// <returns>Todo from database or default</returns>
    Todo Get( int id );

    /// <summary>
    /// Inserts new Todo object into database
    /// </summary>
    /// <returns>Returns id of new obejct</returns>
    int Create( Todo todo );

    /// <summary>
    /// Updates existing todo in the database or do nothing
    /// </summary>
    void Update( Todo todo );

    /// <summary>
    /// Delets Todo from database or do nothing
    /// </summary>
    void Delete( int todo );
}

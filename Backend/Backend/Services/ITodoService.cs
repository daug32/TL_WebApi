using TodoList.Dto;

namespace TodoList.Services;

public interface ITodoService
{
    IEnumerable<TodoDto> GetTodos();

    /// <summary>
    /// Get task with Id
    /// </summary>
    /// <returns>Returns task from Database or default</returns>
    TodoDto GetTodo( int todoId );

    /// <summary>
    /// Registers new task
    /// </summary>
    /// <returns>Returns Id of new task</returns>
    TodoDto CreateTodo( TodoDto todo );

    /// <summary>
    /// Mark the task as completed
    /// </summary>
    /// <returns>Retruns false if task wasn't marked</returns>
    void CompleteTodo( int todoId );

    /// <summary>
    /// Deletes task
    /// </summary>
    void DeleteTodo( int todoId );
}

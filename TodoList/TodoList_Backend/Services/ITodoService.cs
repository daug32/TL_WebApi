using To_Do_List_Backend.Domain;
using To_Do_List_Backend.Dto;

namespace To_Do_List_Backend.Services
{
    public interface ITodoService
    {
        List<Todo> GetTodos();

        /// <summary>
        /// Get task with Id
        /// </summary>
        /// <returns>Returns task from Database or default</returns>
        Todo GetTodo( int todoId );

        /// <summary>
        /// Registers new task
        /// </summary>
        /// <returns>Returns Id of new task</returns>
        int CreateTodo( TodoDto todo );

        /// <summary>
        /// Mark the task as completed
        /// </summary>
        /// <returns>Retruns false if task wasn't marked</returns>
        bool CompleteTodo( int todoId );

        /// <summary>
        /// Deletes task
        /// </summary>
        void DeleteTodo( int todoId );
    }
}

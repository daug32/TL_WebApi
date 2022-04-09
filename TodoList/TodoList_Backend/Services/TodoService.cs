using To_Do_List_Backend.Domain;
using To_Do_List_Backend.Dto;
using To_Do_List_Backend.Repositories;

namespace To_Do_List_Backend.Services
{
    public class TodoService : ITodoService
    {
        private ITodoRepository _todoRepo = new TodoRepository();

        public List<Todo> GetTodos() => _todoRepo.GetTodos();
        
        public Todo GetTodo( int id ) => _todoRepo.Get( id );

        public bool CompleteTodo( int todoId )
        {
            var todo = _todoRepo.Get( todoId );
            if ( todo.Id != todoId ) return false;

            todo.IsDone = true;
            _todoRepo.Update( todo );
            return true;
        }

        public int CreateTodo( TodoDto todoDto )
        {
            var todo = new Todo()
            {
                Id = 0,
                Title = todoDto.Title,
                IsDone = todoDto.IsDone
            };
            return _todoRepo.Create( todo );
        }
    
        public void DeleteTodo( int todoId )
        {
            _todoRepo.Delete( todoId );
        }
    }
}

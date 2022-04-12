using TodoList.Domain;
using TodoList.Dto;
using TodoList.Repositories;

namespace TodoList.Services
{
    public class TodoService : ITodoService
    {
        private ITodoRepository _todoRepo = new TodoRepository();

        public IEnumerable<TodoDto> GetTodos()
        {
            return _todoRepo.GetTodos().Select( el => ModelToDto( el ) );
        }
        
        public TodoDto GetTodo( int id )
        {
            return ModelToDto( _todoRepo.Get( id ) );
        }

        public void CompleteTodo( int todoId )
        {
            var todo = _todoRepo.Get( todoId );
            if ( todo.Id < 1 ) return;
            _todoRepo.Update( todo );
        }

        public TodoDto CreateTodo( TodoDto todoDto )
        {
            var todo = new Todo()
            {
                Id = 0,
                Title = todoDto.Title,
                IsDone = todoDto.IsDone
            };
            todoDto.Id = _todoRepo.Create( todo );
            return todoDto;
        }
    
        public void DeleteTodo( int todoId )
        {
            _todoRepo.Delete( todoId );
        }

        private TodoDto ModelToDto( Todo todo )
        {
            return new TodoDto()
            {
                Id = todo.Id,
                Title = todo.Title,
                IsDone = todo.IsDone
            };
        }
    }
}

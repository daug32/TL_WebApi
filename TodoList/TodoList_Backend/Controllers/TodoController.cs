using System.Text.Json;
using To_Do_List_Backend.Dto;
using Microsoft.AspNetCore.Mvc;
using To_Do_List_Backend.Domain;
using To_Do_List_Backend.Services;

namespace To_Do_List_Backend.Controllers
{
    [Route( "rest/[controller]" )]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private ITodoService _todoService;

        public TodoController()
        {
            _todoService = new TodoService();
        }

        [HttpGet]
        [Route( "get-all" )]
        public IActionResult GetAll()
        {
            var todos = _todoService.GetTodos().Select( el => ConvertToRaw( el ) );
            var json = JsonSerializer.Serialize( todos );
            return Ok( json );
        }

        [HttpGet]
        [Route( "{todoId}" )]
        public IActionResult Get( int todoId )
        {
            var todo = _todoService.GetTodo( todoId );
            var json = JsonSerializer.Serialize( todo );
            return Ok( json );
        }

        [HttpPost]
        [Route( "create" )]
        public IActionResult Create( [FromBody] TodoDto todoDto )
        {
            todoDto.Id = _todoService.CreateTodo( todoDto );
            var json = JsonSerializer.Serialize( todoDto );
            return Ok( json );
        }

        [HttpPut]
        [Route( "{todoId}/complete" )]
        public IActionResult Complete( int todoId )
        {
            var isCompleted = _todoService.CompleteTodo( todoId );
            var json = JsonSerializer.Serialize( isCompleted );
            return Ok( json );
        }

        [HttpDelete]
        [Route( "{todoId}/delete" )]
        public IActionResult Delete( int todoId )
        {
            _todoService.DeleteTodo( todoId );
            return Ok( "true" );
        }

        private TodoDto ConvertToRaw( Todo todo )
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

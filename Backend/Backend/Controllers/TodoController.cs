using TodoList.Dto;
using TodoList.Services;
using Microsoft.AspNetCore.Mvc;

namespace TodoList.Controllers;

[ApiController]
[Route( "rest/[controller]" )]
public class TodoController : ControllerBase
{
    private ITodoService _todoService;

    public TodoController()
    {
        _todoService = new TodoService();
    }

    [HttpGet( "get-all" )]
    public IActionResult GetAll()
    {
        var todoDtos = _todoService.GetTodos();
        return Ok( todoDtos );
    }

    [HttpGet( "get/{todoId}" )]
    public IActionResult Get( int todoId )
    {
        var todoDto = _todoService.GetTodo( todoId );
        return Ok( todoDto );
    }

    [HttpPost( "create" )]
    public IActionResult Create( [FromBody] TodoDto todoDto )
    {
        todoDto = _todoService.CreateTodo( todoDto );
        return Ok( todoDto );
    }

    [HttpPut( "complete/{todoId}" )]
    public IActionResult Complete( int todoId )
    {
        _todoService.CompleteTodo( todoId );
        return Ok();
    }

    [HttpDelete( "delete/{todoId}" )]
    public IActionResult Delete( int todoId )
    {
        _todoService.DeleteTodo( todoId );
        return Ok();
    }
}

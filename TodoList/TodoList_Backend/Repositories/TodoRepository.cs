using System;
using System.Linq;
using System.Threading.Tasks;
using To_Do_List_Backend.Domain;
using System.Collections.Generic;

namespace To_Do_List_Backend.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private string _table = "todo";

        public List<Todo> GetTodos()
        {
            return BaseRepository.GetAll( _table )
                .Select( el => ParseRow( el ) )
                .ToList();
        }

        public Todo Get( int id )
        {
            var row = BaseRepository.GetFirstBy( _table, "id_todo", id );
            if ( row.Count < 1 ) return new Todo();
            return ParseRow( row );
        }

        public int Create( Todo todo )
        {
            return BaseRepository.Insert( _table, ToRow( todo ) );
        }

        public void Delete( int todoId )
        {
            BaseRepository.DeleteBy( _table, "id_todo", todoId );
        }

        public int Update( Todo todo )
        {
            var didSmth = BaseRepository.Update( _table, "id_todo", todo.Id, ToRow( todo ) );
            if ( !didSmth ) return 0;
            return todo.Id;
        }

        protected static Todo ParseRow( Dictionary<string, object> row )
        {
            var id = Convert.ToInt32( row[ "id_todo" ] );
            var title = Convert.ToString( row[ "title" ] );
            var isDone = Convert.ToBoolean( row[ "is_done" ] );

            return new Todo() { Id = id, Title = title, IsDone = isDone };
        }

        protected static Dictionary<string, object> ToRow( Todo todo )
        {
            return new Dictionary<string, object>()
            {
                { "title", todo.Title },
                { "is_done", todo.IsDone }
            };
        }
    }
}

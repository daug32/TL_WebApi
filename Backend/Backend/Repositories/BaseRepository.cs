/*
 * This class contains common methods for handling databases. 
 * It is used by other repository classes. 
 * There most methods return Dictionary<string, object> - 
 * in fact, it is a row saved in columnName/columnValue format
 * 
 * List<Dictionary<string, object>> GetAll( string table );
 * List<Dictionary<string, object>> GetBy( string table, string key, object value );
 * Dictionary<string, object> GetFirstBy( string table, string key, object value );
 * 
 * int Count( string table );
 * int Count( string table, string key, int value );
 * 
 * int Insert( string table, Dictionary<string, object> data );
 * bool Update( string table, string key, object value, Dictionary<string, object> data );
 * 
 * bool DeleteBy( string table, string key, object value ); 
*/

using System.Data.SqlClient;

namespace TodoList.Repositories;

/// <summary>
/// Contains base SQL read operations 
/// </summary>
static class BaseRepository
{

    /// <summary>
    /// Get a collection of rows in table
    /// </summary>
    public static List<Dictionary<string, object>> GetAll( string table )
    {
        var result = new List<Dictionary<string, object>>();

        using ( var connection = new SqlConnection( Config.ConnectionString ) )
        {
            connection.Open();

            using var sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = $"SELECT * FROM [{table}]";

            using var sqlReader = sqlCommand.ExecuteReader();
            while ( sqlReader.Read() )
            {
                var columnCount = sqlReader.FieldCount;
                var item = new Dictionary<string, object>();
                for ( int i = 0; i < columnCount; i++ )
                {
                    var columnName = sqlReader.GetName( i );
                    object columnValue = sqlReader.GetValue( i );
                    item.Add( columnName, columnValue );
                }
                result.Add( item );
            }
        }

        return result;
    }

    /// <summary>
    /// Get a collection of rows where [key] = value. 
    /// Each row is a collection of columnName/columnValue format
    /// </summary>
    public static List<Dictionary<string, object>> GetBy( string table, string key, object value )
    {
        var result = new List<Dictionary<string, object>>();

        using ( var connection = new SqlConnection( Config.ConnectionString ) )
        {
            connection.Open();

            using var sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = $"SELECT * FROM [{table}] WHERE [{key}] = @value";
            sqlCommand.Parameters.AddWithValue( "@value", value );

            using var sqlReader = sqlCommand.ExecuteReader();
            while ( sqlReader.Read() )
            {
                var item = new Dictionary<string, object>();
                var columnCount = sqlReader.FieldCount;
                for ( int i = 0; i < columnCount; i++ )
                {
                    var columnName = sqlReader.GetName( i );
                    var columnValue = sqlReader.GetValue( i );
                    item.Add( columnName, columnValue );
                }
                result.Add( item );
            }
        }

        return result;
    }

    /// <summary>
    /// Get a row where [key] = value. Row is a collection of columnName/columnValue format
    /// </summary>
    public static Dictionary<string, object> GetFirstBy( string table, string key, object value )
    {
        var result = new Dictionary<string, object>();

        using ( var connection = new SqlConnection( Config.ConnectionString ) )
        {
            connection.Open();

            using var sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = $"SELECT TOP 1 * FROM [{table}] WHERE [{key}] = @value ORDER BY [{key}] ";
            sqlCommand.Parameters.AddWithValue( "@value", value );

            using var sqlReader = sqlCommand.ExecuteReader();
            if ( sqlReader.Read() )
            {
                var columnCount = sqlReader.FieldCount;
                for ( int i = 0; i < columnCount; i++ )
                {
                    var columnName = sqlReader.GetName( i );
                    var columnValue = sqlReader.GetValue( i );
                    result.Add( columnName, columnValue );
                }
            }
        }

        return result;
    }

    /// <summary>
    /// Returns number of rows where [key] = value
    /// </summary>
    public static int Count( string table, string key, object value )
    {
        var result = 0;

        using ( var connection = new SqlConnection( Config.ConnectionString ) )
        {
            connection.Open();

            using var sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = $"SELECT COUNT(*) FROM [{table}] WHERE [{key}] = @value";
            sqlCommand.Parameters.AddWithValue( "@value", value );

            result = Convert.ToInt32( sqlCommand.ExecuteScalar() );
        }

        return result;
    }

    /// <summary>
    /// Returns number of rows in table
    /// </summary>
    public static int Count( string table )
    {
        var result = 0;

        using ( var connection = new SqlConnection( Config.ConnectionString ) )
        {
            connection.Open();

            using var sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = $"SELECT COUNT(*) FROM [{table}]";

            result = Convert.ToInt32( sqlCommand.ExecuteScalar() );
        }

        return result;
    }

    /// <summary>
    /// Deletes every row where [key] = value
    /// </summary>
    public static bool DeleteBy( string table, string key, object value )
    {
        var didSmth = false;

        using var connection = new SqlConnection( Config.ConnectionString );
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = $"DELETE [{table}] where [{key}] = @value";
        command.Parameters.AddWithValue( "@value", value );
        didSmth = command.ExecuteNonQuery() > 0;

        return didSmth;
    }

    /// <summary>
    /// Insert new row filled by given data into table.
    /// </summary>
    /// <returns>Returns an id of the inserted row or -1</returns>
    public static int Insert( string table, Dictionary<string, object> data )
    {
        var result = -1;
        // keys = "@key1, @key2, ..., @keyN"
        var keys = "@" + data.Keys.Aggregate( ( keyString, next ) => $"{keyString}, @{next}" );

        var connection = new SqlConnection( Config.ConnectionString );
        {
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = $"INSERT INTO [{table}] VALUES ({keys}) SELECT SCOPE_IDENTITY()";
            for ( int i = 0; i < data.Count; i++ )
            {
                var item = data.ElementAt( i );
                command.Parameters.AddWithValue( $"@{item.Key}", item.Value );
            }

            result = Convert.ToInt32( command.ExecuteScalar() );
        }

        return result;
    }

    /// <summary>
    /// Update row, which contains [key] = value, by the given data.
    /// </summary>
    /// <returns>Returns an id of the row or -1</returns>
    public static bool Update( string table, string key, object value, Dictionary<string, object> data )
    {
        var didSmth = false;
        // keys = "[key1] = @key1, [key2] = @key2, ..., [keyN] = @keyN"
        var keys = data.Keys.Aggregate( "", ( keyString, next ) => $"{keyString}, [{next}] = @{next}" ).Remove( 0, 2 );

        var connection = new SqlConnection( Config.ConnectionString );
        {
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = $"UPDATE [{table}] SET {keys} WHERE [{key}] = @value";
            for ( int i = 0; i < data.Count; i++ )
            {
                var item = data.ElementAt( i );
                command.Parameters.AddWithValue( $"@{item.Key}", item.Value );
            }
            command.Parameters.AddWithValue( "@value", value );

            didSmth = command.ExecuteNonQuery() > 0;
        }
        return didSmth;
    }
}

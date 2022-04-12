namespace TodoList.Repositories;

public static class Config
{
    public static readonly string ConnectionString =
        @"Data Source=DESKTOP-SBPQHAO\SQLEXPRESS;
        Initial Catalog=Todo;
        Pooling=true;
        Integrated Security=SSPI";
}

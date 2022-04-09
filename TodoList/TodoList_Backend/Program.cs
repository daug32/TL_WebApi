using To_Do_List_Backend.Repositories;
using To_Do_List_Backend.Services;

var builder = WebApplication.CreateBuilder( args );

builder.Services.AddCors( options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins( "http://localhost:4200" )
                                .AllowAnyHeader()
                                .AllowAnyMethod();
        } );
} );

builder.Services.AddControllers();
//Тут добавить сервис IToDoService в DI

var app = builder.Build();
app.MapControllers();
app.UseCors( builder =>
{
    builder
    .WithOrigins( "localhost:4200" )
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
} );
app.Run();
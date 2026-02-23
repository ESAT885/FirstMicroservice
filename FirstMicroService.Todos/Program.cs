using FirstMicroService.Todos.Context;
using FirstMicroService.Todos.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseInMemoryDatabase("MyDb");
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapGet("/todos/getall", (ApplicationDbContext context) =>
{
    var todos = context.Todos.ToList();
    return todos;
});
app.MapPost("/todos/post", (string work,ApplicationDbContext context) =>
{
    var todo = new Todo
    {
        Work = work,
    };
    context.Add(todo);
    context.SaveChanges();
    return new { Message = "Todo createis succeddful" };
});
app.Run();


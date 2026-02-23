using FirstMicroService.Categories.WebAPI.Context;
using FirstMicroService.Categories.WebAPI.Dtos;
using FirstMicroService.Categories.WebAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGet("/category/getall", async(ApplicationDbContext context,CancellationToken cancellationToken) =>
{
    var categories = await context.Categories.ToListAsync(cancellationToken);
    return categories;
});
app.MapPost("/category", async (
    CreateCategoryDto request,
    ApplicationDbContext context,
    CancellationToken cancellationToken) =>
{
    bool isNameExists = await context.Categories
        .AnyAsync(p => p.Name.ToLower() == request.Name.ToLower(), cancellationToken);

    if (isNameExists)
        return Results.BadRequest(new { message = "Category already exists" });

    var category = new Category
    {
        Name = request.Name
    };

    context.Categories.Add(category);
    await context.SaveChangesAsync(cancellationToken);

    return Results.Created($"/category/{category.Id}", new
    {
        message = "Category created successfully",
        categoryId = category.Id
    });
});

using(var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();
}
app.Run();


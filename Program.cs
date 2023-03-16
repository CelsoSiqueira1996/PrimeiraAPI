var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Use(async (context, next) =>
{
    if (!context.Request.Headers.TryGetValue("Profile", out var perfil))
    {
        context.Response.StatusCode = 400;
        return;
    }

    bool metodoAdmin = (context.Request.Method == "POST" ||
                            context.Request.Method == "PUT" ||
                            context.Request.Method == "DELETE"
                            );

    if (metodoAdmin && perfil != "Admin")
    {
        context.Response.StatusCode = 401;
        return;
    }

    if (context.Request.Method == "GET" &&
        context.Request.Path != "/api/person/" &&
        perfil != "Professor" &&
        perfil != "Admin")
    {
        context.Response.StatusCode = 401;
        return;
    }

    await next.Invoke();
});

app.UseAuthorization();

app.MapControllers();

app.Run();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); 
builder.Services.AddControllers();
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();


app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.UseHttpsRedirection();

app.MapGet("/", () => Results.Content(
    "<h1>Welcome to the API1</h1><p>Try <a href='/swagger'>Swagger UI</a></p>", 
    "text/html"));

app.Run();

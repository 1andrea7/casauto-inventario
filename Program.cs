using CasautoAPI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CasautoContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CasautoConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTodo", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseDefaultFiles();      // ← busca index.html por defecto
app.UseStaticFiles();       // ← sirve archivos de wwwroot
app.UseCors("PermitirTodo");
app.UseAuthorization();
app.MapControllers();

app.Run();
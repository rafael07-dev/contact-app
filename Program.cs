using ContactApp.Data;
using ContactApp.Exceptions;
using ContactApp.Interfaces;
using ContactApp.Repository;
using ContactApp.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

internal class Program
{
    private static void Main(string[] args)
    {

        var options = new WebApplicationOptions
        {
            WebRootPath = "wwwroot"
        };
        var builder = WebApplication.CreateBuilder(options);

        // Add services to the container.
        builder.Services.AddScoped<IContactRepository, ContactRepository>();
        builder.Services.AddScoped<IContactService, ContactService>();
        builder.Services.AddScoped<IFileService, FileService>();
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("PermitirFrontend",
                policy =>
                {
                    policy.WithOrigins("http://localhost:5173") // Tu frontend
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
        });


        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(Path.Combine(app.Environment.WebRootPath, "uploads")), // Ruta absoluta
            RequestPath = "/uploads"  // Ruta accesible desde el navegador
        });

        app.UseMiddleware<ExceptionHandlingMiddleware>();

        app.UseHttpsRedirection();

        app.UseCors("PermitirFrontend");

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}

using Microsoft.EntityFrameworkCore;
using WK_34.Data;
using WK_34.Data.Interfaces;
using WK_34.Repositories;
using WK_34.Repositories.Interfaces;
using WK_34.Services;
using WK_34.Services.Interfaces;

namespace WK_34
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddDbContext<LibraryContext>(
             options =>
             {
                 options.UseSqlServer(builder.Configuration.GetConnectionString(nameof(LibraryContext)));
             });


            builder.Services.AddScoped<IAuthorsRepository, AuthorsRepository>();
            builder.Services.AddScoped<IAuthorsService, AuthorsService>();
            builder.Services.AddScoped<IBooksRepository, BooksRepository>();
            builder.Services.AddScoped<IBooksService, BooksService>();

            builder.Services.AddScoped<IAuthorValidator, AuthorValidator>();
            builder.Services.AddScoped<IBookValidator, BookValidator>();

            // Add services to the container.

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

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<LibraryContext>();
                var config = services.GetRequiredService<IConfiguration>();

                LibraryInitializer.Initialize(context, config);
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

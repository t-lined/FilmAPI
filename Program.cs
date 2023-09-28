using FilmAPI.Models;
using FilmAPI.Services.Characters;
using FilmAPI.Services.Franchises;
using FilmAPI.Services.Movies;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace FilmAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Film API",
                    Description = "API for managing films",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "ME",
                        Url = new Uri("https://example.com/contact")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Example License",
                        Url = new Uri("https://example.com/license")
                    }
                });
                // using System.Reflection;
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });



            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            // Register services here
            builder.Services.AddScoped<ICharacterService, CharacterService>();
            builder.Services.AddScoped<IFranchiseService, FranchiseService>();
            builder.Services.AddScoped<IMovieService, MovieService>();

            builder.Services.AddDbContext<FilmAPIDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("FilmAPI"));
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}

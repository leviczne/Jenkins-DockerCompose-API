using Microsoft.EntityFrameworkCore;
using SistemaDeCadastroAPI.Data;
using SistemaDeCadastroAPI.Repositorios;
using SistemaDeCadastroAPI.Repositorios.Intefaces;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SistemaDeCadastroAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors();
              


            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddEntityFrameworkSqlServer()
                .AddDbContext<SistemaCadastroDBContex>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("DataBase"))
                );
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });
            builder.Services.AddScoped<IUsuarioRepositorio,UsuarioRepositorio>();
            builder.Services.AddScoped<IViagemRepositorio,ViagemRepositorio>();
            builder.Services.AddScoped<IUsuariosViagemRepositorio, UsuariosViagemRepositorio>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(c =>
            {
                c.AllowAnyHeader();
                c.AllowAnyMethod();
                c.AllowAnyOrigin();

            });
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace ApiGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            IConfiguration config = new ConfigurationBuilder().AddJsonFile("Ocelot.json", optional: false, reloadOnChange: true).Build();
            builder.Services.AddOcelot(config);
            var app = builder.Build();

            app.MapGet("/", () => "Hello World!");
            app.UseOcelot();
            app.Run();
        }
    }
}

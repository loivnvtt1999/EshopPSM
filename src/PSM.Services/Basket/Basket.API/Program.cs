
namespace Basket.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var assembly = typeof(Program).Assembly;
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddCarter();
            builder.Services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(assembly);
                config.AddOpenBehavior(typeof(ValidationBehavior<,>));
                config.AddOpenBehavior(typeof(LoggingBehavior<,>));
            });

            var app = builder.Build();
            app.MapCarter();
            app.Run();
        }
    }
}

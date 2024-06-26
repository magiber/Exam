namespace BackEndApi
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

            // Cors
            builder.Services.AddCors(options => options.AddPolicy("AllowWebapp",
                                                builder => builder.AllowAnyOrigin()
                                                                .AllowAnyHeader()
                                                                .AllowAnyMethod()));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("AllowWebapp");

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

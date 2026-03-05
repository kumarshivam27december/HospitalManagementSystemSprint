namespace Hospital.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            app.UseStaticFiles();

            app.MapGet("/", context =>
            {
                context.Response.Redirect("/pages/index.html");
                return Task.CompletedTask;
            });

            app.Run();
        }
    }
}

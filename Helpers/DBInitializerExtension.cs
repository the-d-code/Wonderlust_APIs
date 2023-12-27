using WONDERLUST_PROJECT_APIs.Data;

namespace WONDERLUST_PROJECT_APIs.Helpers
{
    public static class DBInitializerExtension
    {
        public static IApplicationBuilder UseSeedDB(this IApplicationBuilder app)
        {
            ArgumentNullException.ThrowIfNull(app, nameof(app));

            using var scope = app.ApplicationServices.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<DataContext>();
            DBSeeder.Seed(context);

            return app;
        }
    }
}

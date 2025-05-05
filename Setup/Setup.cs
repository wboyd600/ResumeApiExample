using ResumeApiExample.Services;

namespace ResumeApiExample.Setup;

public static partial class Setup
{
    public static void ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();

        builder.SetupServices();
    }

    public static void SetupServices(this WebApplicationBuilder builder)
    {
        // services
        builder.Services.AddScoped<IPdfService, PdfService>();
    }
}

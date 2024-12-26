namespace CustomEmailTemplate.Application;

public static class Startup
{
    /// <summary>
    /// Register all services that needed in this Layer (Class Library)
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static WebApplicationBuilder BuildApplication(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddControllersWithViews()
            .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
            .AddDataAnnotationsLocalization()
            .Services
            .AddLocalization()
            // need it to get context and cancellation token
            .AddHttpContextAccessor()

        #region Register CancellationToken so it doesn't need to be passed explicitly in every method.

            .AddScoped(typeof(CancellationToken),
                (serviceProvider) =>
                    serviceProvider.GetService<IHttpContextAccessor>()?.HttpContext?.RequestAborted ??
                    CancellationToken.None);

        #endregion
        //.AddScoped<IExecuteViewAsPdfService, ExecuteViewAsPdfService>()
        //.AddScoped<IEmailTemplateService, EmailTemplateService>()
        //.AddScoped<IEmailSenderService, EmailSenderService>()
        //.AddScoped<IOrderService, OrderService>();

        #region Register all services with reflection

        var allInterfaces = typeof(IEmailTemplateService).Assembly.GetTypes().Where(a => a.IsInterface).ToList();
        var allClasses = typeof(EmailTemplateService).Assembly.GetTypes().Where(a => a.IsClass).ToList();

        foreach (var type in allInterfaces)
        {
            var classType = allClasses.FirstOrDefault(type.IsAssignableFrom);
            if (classType == null) continue;
            builder.Services.AddScoped(type, classType);
        }

        #endregion

        return builder;
    }

    /// <summary>
    /// add needed Middlewares in this Layer
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseApplication(this IApplicationBuilder app)
    {
        var supportedCultures = new[] { "en", "ar" }; // Add languages as needed
        var localizationOptions = new RequestLocalizationOptions()
            .SetDefaultCulture("en")
            .AddSupportedCultures(supportedCultures)
            .AddSupportedUICultures(supportedCultures);

        app.UseRequestLocalization(localizationOptions);
        return app;
    }
}
namespace Orders.API.Configurations
{
    public static class ApplicationConfig
    {
        public static void AddApplicationConfig(this WebApplicationBuilder builder)
        {
            builder.Services.AddHttpContextAccessor();
        }
    }
}

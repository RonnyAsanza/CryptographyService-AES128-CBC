using Microsoft.AspNetCore.Cors.Infrastructure;

namespace Encrypted_and_Decrypted.NewFolder
{
    public class DataConfiguration
    {
        public static void Configure(IServiceCollection services)
        {
            ConfigureApplication(services);
            ConfigureExternalServices(services);
        }
        private static void ConfigureApplication(IServiceCollection services)
        {
            services.AddScoped<ICryptographyService, CryptographyService>();
        }
        private static void ConfigureExternalServices(IServiceCollection services)
        {
        }
    }
}

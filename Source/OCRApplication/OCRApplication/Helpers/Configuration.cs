using Microsoft.Extensions.Configuration;

namespace OCRApplication.Helpers
{
    public static class Configuration
    {
        public static IConfigurationRoot Config()
        {

            //Configuration to read from json file, and environment variables
            var builder = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
           .AddEnvironmentVariables();

            IConfigurationRoot configuration = builder.Build();

            return configuration;
        }
    }
}

using Microsoft.Extensions.Configuration;

namespace ProgrammersBlog.Data.Concrete;

public static class Configuration
{
    static public string ConnectionString 
    {
        get
        {
            ConfigurationManager configurationManager = new();
            configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(),"../ProgrammersBlog.Mvc"));
            configurationManager.AddJsonFile("appsettings.json");
            return configurationManager.GetConnectionString("SqlConnection");
        }
    }
}
using System.IO;
using Microsoft.Extensions.Configuration;

public static class ConfigurationProvider
{
    public static TAEssentials.UI.DataClasses.Configuration.Configuration GetConfiguration()
    {
        return GetConfigurationFile<TAEssentials.UI.DataClasses.Configuration.Configuration>("appsettings.json", "Configuration");
    }

    public static T GetConfigurationFile<T>(string jsonFileName, string sectionName)
    {
        return new ConfigurationBuilder().SetBasePath(GetSolutionRootPath())
            //.AddEnvironmentVariables()
            .AddJsonFile(jsonFileName)
            .Build()
            .GetSection(sectionName)
            .Get<T>();
    }

    private static string GetSolutionRootPath()
    {
        return new DirectoryInfo(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName).ToString();
    }
}